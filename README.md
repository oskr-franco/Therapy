#  Therapy Exercise API

This is a .NET Core API for managing physical therapy exercises. The API provides operations for creating, reading, updating, and deleting exercises. Each exercise can have associated media like videos and images.

## Swagger

To view the Swagger documentation for this API, navigate to `/swagger` in your browser.

## Project Structure

The solution is structured into three projects following Clean Architecture principles:

- **Therapy.API**: The entry point of the application. It contains the web API controllers.
- **Therapy.Core**: This project contains the business logic, entities, and interfaces (or abstractions).
- **Therapy.Domain**: This project contains the domain entities and any shared domain logic.
- **Therapy.Infrastructure**: This project implements the interfaces defined in the core layer. It contains concrete classes and is responsible for all external concerns, such as database access, file system access, network calls, etc.

## Installation

Make sure you have the [.NET Core SDK](https://dotnet.microsoft.com/download) installed on your machine. Then, clone this repository to your local machine:

```bash
git clone https://github.com/oskr-franco/Therapy.git
```

## Restore

```bash
dotnet restore
```

## Publish 

```bash
dotnet publish -o site
dotnet publish -c Release -o ./app/ Therapy.API/Therapy.API.csproj
```
## Database

You'll need to update the connection string in the appsettings.json file in the Therapy.API project to point to your SQL Server instance.

Finally, run the database migrations to create the database schema:
```bash
dotnet ef migrations add {NewMigrationName} --project {YourDataProject} --startup-project {YourStartupProject}
dotnet ef database update
```
Example
```bash
dotnet ef migrations add InitialCreate --project Therapy.Infrastructure --startup-project Therapy.API
dotnet ef database update --project Therapy.Infrastructure --startup-project Therapy.API
```

## Running the API

To run the API, navigate to the root of the repository and run:

```bash
dotnet run --project Therapy.API
```

## Sync and Deploy Workflows

### Branch Synchronization (Sync Main into Dev)

To keep the `dev` branch in sync with the `main` branch, a GitHub Actions workflow is set up. This workflow automatically merges changes from `main` to `dev`. If the automatic sync fails, you can manually sync by checking out the `dev` branch and running:

```bash
git checkout dev
git merge main
```

### Automatic Deployment to AWS ECS

Deployments are triggered automatically when changes are pushed to the `dev` or `main` branches. If you need to perform a manual deployment, you can use the following commands:

```bash
# Manual deployment steps
```

**Security Considerations**: Ensure that no sensitive information or secrets are exposed in your deployment configurations. Always use environment variables or secrets management tools to handle sensitive data.

## Scripts Usage

To use the `scripts.sh` file, source it in your terminal:

```bash
source scripts.sh
```

You can then run predefined functions as needed.

### Available Functions in `scripts.sh`

#### General Commands
- `run`: Runs the API.
- `build`: Builds the project.

#### Package Management
- `add_package_api <package>`: Adds a package to `Therapy.API`.
- `add_package_core <package>`: Adds a package to `Therapy.Core`.
- `add_package_infra <package>`: Adds a package to `Therapy.Infrastructure`.
- `add_package_domain <package>`: Adds a package to `Therapy.Domain`.

#### Database Management
- `add_migration <name>`: Adds a new migration.
- `update_database`: Updates the database schema.

#### Docker Commands
- `build_docker`: Builds the Docker image.
- `run_docker`: Runs the API inside a Docker container.