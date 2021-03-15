#!/usr/bin/env bash

aws cloudformation create-stack \
  --stack-name exchange-rate-api-service \
  --template-body file://service.yml \
  --parameters \
      ParameterKey=StackName,ParameterValue=exchange-rate-api-network \
      ParameterKey=ServiceName,ParameterValue=exchange-rate-api \
      ParameterKey=ImageUrl,ParameterValue=031388629412.dkr.ecr.us-east-1.amazonaws.com/exchange-rate-api:latest \
      ParameterKey=HealthCheckIntervalSeconds,ParameterValue=90
