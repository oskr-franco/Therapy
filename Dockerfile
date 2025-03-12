# Use the official .NET Core SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the solution and project files to restore dependencies
COPY *.sln ./
COPY Therapy.API/Therapy.API.csproj Therapy.API/
COPY Therapy.Core/Therapy.Core.csproj Therapy.Core/
COPY Therapy.Infrastructure/Therapy.Infrastructure.csproj Therapy.Infrastructure/
COPY Therapy.Domain/Therapy.Domain.csproj Therapy.Domain/

# Restore dependencies
RUN dotnet restore

# Copy all source files and build the project
COPY . .

# Build the application
WORKDIR /app/Therapy.API
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
WORKDIR /app/Therapy.API
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET Core runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port and set the entry point
#EXPOSE 5050

# Set default ASP.NET Core URLs inside the container
ENV ASPNETCORE_URLS="http://+:5050"

# Set environment variable for runtime
#ENV ASPNETCORE_ENVIRONMENT=Production
ARG ENVIRONMENT=Development
ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT

# Command to run the application
ENTRYPOINT ["dotnet", "Therapy.API.dll"]