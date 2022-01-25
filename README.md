# Exchange Rate API

![](https://github.com/yagoluiz/exchange-rate-api/workflows/Docker%20Image%20CI/badge.svg)

API responsible for making foreign currency quotes for BRL (Brazilian Real).

## Instructions for run project

Run project via Docker, via Visual Studio (F5 or CTRL + F5), Visual Studio Code (tasks project) or CLI.

### Docker

```bash
docker-compose up -d
```

### .NET CLI

- Run project

```bash
src/Exchange.Rate.API

dotnet watch run
```

- Run tests

```bash
dotnet test -t
```

## Endpoints

*GET /quote*

```bash
curl -X GET "http://localhost:5000/api/v1/exchange-rates/quote?amount=10&currency=USD" -H  "accept: application/json"
```

*GET /conversion*

```bash
curl -X GET "http://localhost:5000/api/v1/exchange-rates/conversion?amount=10&currency=USD&customerSegment=PRIVATE" -H  "accept: application/json"
```
For more information visit swagger: *http://localhost:5000/swagger/index.html*

## Deploy

### AWS arquitecture reference

![](https://raw.githubusercontent.com/thombergs/code-examples/master/aws/cloudformation/ecs-in-two-public-subnets/ecs-in-two-public-subnets.svg)

*[https://github.com/thombergs/code-examples/blob/master/aws/cloudformation/ecs-in-two-public-subnets/ecs-in-two-public-subnets.svg](https://github.com/thombergs/code-examples/blob/master/aws/cloudformation/ecs-in-two-public-subnets/ecs-in-two-public-subnets.svg)*

Step by step to manual deploy using **AWS CloudFormation**:

1. Create Docker image repository

This project uses the AWS ECR (Docker Container Registry) as a Docker image repository. Other repositories can be used, such as the Docker Hub.

- Execute docker-compose file:

```bash
docker-compose up -d
```

Enter the folder *scripts/aws/ecr* and run the scripts:

- Run script to create AWS ECR:

```bash
sh ecr-deploy.sh
```

- Run script to deploy the AWS ECR image. Necessary to include AWS **AccountId** as a parameter:

```bash
sh image-deploy.sh 999999999999
```

2. Create networks

Enter the folder *scripts/aws/networks* and run the scripts:

- Run script to create and deploy networks

```bash
sh network-deploy.sh
```

3. Create services

Check the status of network creation on AWS CloudFormation. Run this step only when the status is completed: **CREATE_COMPLETE**.

Enter the folder *scripts/aws/services* and run the scripts:

- Run script to create and deploy services. Necessary to include AWS **AccountId** as a parameter:

```bash
sh service-deploy.sh 999999999999
```

The deployment of this project via AWS CloudFormation was based on the following references:

- *https://reflectoring.io/aws-cloudformation-deploy-docker-image/*
- *https://github.com/thombergs/code-examples/tree/master/aws/cloudformation/ecs-in-two-public-subnets*

### Delete deploy

```bash
aws cloudformation delete-stack \
  --stack-name exchange-rate-api-service 
```

Run delete stack service step only when the status is completed: **CREATE_COMPLETE**.

```bash
aws cloudformation delete-stack \
  --stack-name exchange-rate-api-network 
```
