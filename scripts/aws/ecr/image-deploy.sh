#!/usr/bin/env bash

if [ -z $1 ]; then
  echo "Missing argument: AWS accountId is required"
  exit 0
fi

account=$1

aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin $account.dkr.ecr.us-east-1.amazonaws.com

docker tag exchange-rate-api:latest $account.dkr.ecr.us-east-1.amazonaws.com/exchange-rate-api:latest

docker push $account.dkr.ecr.us-east-1.amazonaws.com/exchange-rate-api:latest
