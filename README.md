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