# Physical Therapy Exercise API

This is a .NET Core API for managing physical therapy exercises. The API provides operations for creating, reading, updating, and deleting exercises. Each exercise can have associated media like videos and images.

## Project Structure

The solution is structured into three projects following Clean Architecture principles:

- **PhysicalTherapy.API**: The entry point of the application. It contains the web API controllers.
- **PhysicalTherapy.Core**: This project contains the business logic, entities, and interfaces (or abstractions).
- **PhysicalTherapy.Domain**: This project contains the domain entities and any shared domain logic.
- **PhysicalTherapy.Infrastructure**: This project implements the interfaces defined in the core layer. It contains concrete classes and is responsible for all external concerns, such as database access, file system access, network calls, etc.

## Installation

Make sure you have the [.NET Core SDK](https://dotnet.microsoft.com/download) installed on your machine. Then, clone this repository to your local machine:

```bash
git clone https://github.com/oskr-franco/physical-therapy-api.git
```

## Restore

```bash
dotnet restore
```

## Database

You'll need to update the connection string in the appsettings.json file in the PhysicalTherapy.API project to point to your SQL Server instance.

Finally, run the database migrations to create the database schema:

```bash
dotnet ef migrations add InitialCreate --project PhysicalTherapy.Infrastructure --startup-project PhysicalTherapy.API
dotnet ef database update --project PhysicalTherapy.Infrastructure --startup-project PhysicalTherapy.API
```

## Running the API

To run the API, navigate to the root of the repository and run:

```bash
dotnet run --project .\PhysicalTherapy.API\PhysicalTherapy.API.csproj
```