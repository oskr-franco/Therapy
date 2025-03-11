#!/bin/bash

function run() {
  dotnet run --project Therapy.API
}

function build(){
  dotnet build
}

function add_package_api() {
  if [ -z "$1" ]
    then
        echo "Error: No package name provided."
        return 1
    fi
  dotnet add Therapy.API/Therapy.API.csproj package $1
}

function add_package_core() {
  if [ -z "$1" ]
    then
        echo "Error: No package name provided."
        return 1
    fi
  dotnet add Therapy.Core/Therapy.Core.csproj package $1
}

function add_package_infra() {
  if [ -z "$1" ]
    then
        echo "Error: No package name provided."
        return 1
    fi
  dotnet add Therapy.Infrastructure/Therapy.Infrastructure.csproj package $1
}

function add_package_domain() {
  if [ -z "$1" ]
    then
        echo "Error: No package name provided."
        return 1
    fi
  dotnet add Therapy.Domain/Therapy.Domain.csproj package $1
}

function add_migration() {
  if [ -z "$1" ]
    then
        echo "Error: No migration name provided."
        return 1
    fi
  dotnet ef migrations add $1 --startup-project Therapy.API --project Therapy.Infrastructure
}

function update_database() {
  dotnet ef database update --startup-project therapy.API --project Therapy.Infrastructure
}

function build_docker() {
  docker build -t therapy-api .
}

function run_docker() {
  docker run -p 8080:5050 -e ASPNETCORE_ENVIRONMENT=Development therapy-api
}