using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using Exchange.Rate.API.Filters;
using Exchange.Rate.API.Middlewares;
using Exchange.Rate.API.Services;
using Exchange.Rate.API.Services.Interfaces;
using Exchange.Rate.Domain.Contracts;
using Exchange.Rate.Domain.Interfaces.Notifications;
using Exchange.Rate.Domain.Interfaces.Services;
using Exchange.Rate.Domain.Notifications;
using Exchange.Rate.Infra.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;

namespace Exchange.Rate.API
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(options =>
            {
                options.Filters.Add<DomainNotificationFilter>();
            });
            services.AddResponseCompression();
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddSwaggerGen(options =>
            {
                options.SchemaFilter<OpenApiShemaFilter>();
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Exchange Rates API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            AddSettings(services);
            AddHttpClientForeignExchangeRates(services);
            AddScopes(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
            app.UseResponseCompression();
            app.UseRouting();
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new ErrorHandlerMiddleware(env).Invoke
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSettings(IServiceCollection services)
        {
            services.Configure<ExchangeRatePerSegment>(Configuration.GetSection("Domain:ExchangeRatePerSegment"));
        }

        private void AddHttpClientForeignExchangeRates(IServiceCollection services)
        {
            services.AddHttpClient<IForeignExchangeRatesService, ForeignExchangeRatesService>((serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri(Configuration["Config:API:ForeignExchangeRates"]);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.OrResult(response =>
                    (int)response.StatusCode == (int)HttpStatusCode.InternalServerError)
              .WaitAndRetryAsync(3, retry =>
                   TimeSpan.FromSeconds(Math.Pow(2, retry)) +
                   TimeSpan.FromMilliseconds(new Random().Next(0, 100))))
              .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(
                   handledEventsAllowedBeforeBreaking: 3,
                   durationOfBreak: TimeSpan.FromSeconds(30)
            ));
        }

        private static void AddScopes(IServiceCollection services)
        {
            services.AddScoped<IDomainNotification, DomainNotification>();
            services.AddScoped<IExchangeRateService, ExchangeRateService>();
        }
    }
}
