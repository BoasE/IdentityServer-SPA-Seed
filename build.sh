#!/usr/bin/env bash


cd ./src/IdP

docker build -f ./Dockerfile.IdentityServer . -t idsrvspaseed_identityserver
docker build -f ./Dockerfile.IdentityServerAdministration . -t idsrvspaseed_identityserver_administration