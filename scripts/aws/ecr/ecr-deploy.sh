#!/usr/bin/env bash

aws cloudformation create-stack \
  --stack-name exchange-rate-repository \
  --template-body file://erc.yml

