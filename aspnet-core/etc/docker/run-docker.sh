#!/bin/bash

if [[ ! -d certs ]]
then
    mkdir certs
    cd certs/
    if [[ ! -f localhost.pfx ]]
    then
        dotnet dev-certs https -v -ep localhost.pfx -p 76e8b26e-9052-4b40-97e4-06cae03ed5be -t
    fi
    cd ../
fi

docker-compose up -d
