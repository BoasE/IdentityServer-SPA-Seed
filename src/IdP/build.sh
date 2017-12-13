#!/usr/bin/env bash

docker build -f Dockerfile.IdentityServer . -t identityserver
#docker build -f Dockerfile.IdentityServerAdministration . -t identityserver.administration
docker-compose up