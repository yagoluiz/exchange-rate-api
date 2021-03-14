using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Exchange.Rate.Domain.Interfaces.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NewtonsoftJson = Newtonsoft.Json;

namespace Exchange.Rate.API.Filters
{
    public class DomainNotificationFilter : IAsyncResultFilter
    {
        private readonly IDomainNotification _domainNotification;

        public DomainNotificationFilter(IDomainNotification domainNotification)
        {
            _domainNotification = domainNotification;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid || _domainNotification.HasNotifications)
            {
                var validations = !context.ModelState.IsValid ?
                    NewtonsoftJson.JsonConvert.SerializeObject(context.ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)) :
                    NewtonsoftJson.JsonConvert.SerializeObject(_domainNotification.Notifications
                        .Select(x => x.Value));

                var problemDetails = new ProblemDetails
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Instance = context.HttpContext.Request.Path.Value,
                    Detail = validations
                };

                context.HttpContext.Response.StatusCode = problemDetails.Status.Value;
                context.HttpContext.Response.ContentType = "application/problem+json";

                using var writer = new Utf8JsonWriter(context.HttpContext.Response.Body);
                JsonSerializer.Serialize(writer, problemDetails);
                await writer.FlushAsync();

                return;
            }

            await next();
        }
    }
}
