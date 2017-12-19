#!/usr/bin/env bash

export HOST_IP=$(node $(pwd)/src/tools/ip.js)
env | grep HOST_IP
cd ./src/IdP/src/IdentityServer.UserClient/
HOST_IP=${HOST_IP} dotnet run