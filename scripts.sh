#!/bin/bash


# Function to set environment variables
function set_environment() {
  ENVIRONMENT=${1:-dev}
  if [[ "$ENVIRONMENT" == "prod" ]]; then
    docker_tag="therapy-api-prod"
    env_arg="Production"
  else
    docker_tag="therapy-api-dev"
    env_arg="Development"
  fi

  # Print the variables
  echo "ENVIRONMENT: $ENVIRONMENT"
  echo "env_arg: $env_arg"
  echo "docker_tag: $docker_tag"
}

# Call the function to set environment variables
set_environment $1

# Scripts Usage:
# - How to source the script: source scripts.sh
# - Running predefined functions.

# Available Functions in `scripts.sh`:
# - General Commands:
#   - run: Runs the API.
#   - build: Builds the project.
# - Package Management:
#   - add_package_api <package>: Adds a package to Therapy.API.
#   - add_package_core <package>: Adds a package to Therapy.Core.
#   - add_package_infra <package>: Adds a package to Therapy.Infrastructure.
#   - add_package_domain <package>: Adds a package to Therapy.Domain.
# - Database Management:
#   - add_migration <name>: Adds a new migration.
#   - update_database: Updates the database schema.
# - Docker Commands:
#   - build_docker: Builds the Docker image.
#   - run_docker: Runs the API inside a Docker container.

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
  docker build -t "$docker_tag" --build-arg ENVIRONMENT="$env_arg" .
}

function run_docker() {
  docker run -p 8080:5050 $docker_tag
}