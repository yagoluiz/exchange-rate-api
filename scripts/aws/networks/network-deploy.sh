#!/usr/bin/env bash

aws cloudformation create-stack \
  --stack-name exchange-rate-api-network \
  --template-body file://network.yml \
  --capabilities CAPABILITY_IAM
