#!/usr/bin/env bash

if [ -z $1 ]; then
  echo "Missing argument: AWS accountId is required"
  exit 0
fi

account=$1

aws cloudformation create-stack \
  --stack-name exchange-rate-api-service \
  --template-body file://service.yml \
  --parameters \
      ParameterKey=StackName,ParameterValue=exchange-rate-api-network \
      ParameterKey=ServiceName,ParameterValue=exchange-rate-api \
      ParameterKey=ImageUrl,ParameterValue=$account.dkr.ecr.us-east-1.amazonaws.com/exchange-rate-api:latest \
      ParameterKey=HealthCheckIntervalSeconds,ParameterValue=90
