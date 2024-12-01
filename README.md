# ASP.NET Core API with Dapper

## Requirements

- **Platform**: x86-64, Linux/WSL
- **Programming Language**: [Dotnet 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)


## Allocate resources & run API
The shell script [up](up.sh) creates a Postgres container via [docker-compose](infra/db/heroes/docker-compose.yml) and serves our ASP.NET API on port 5000.

## Deallocate resources
The shell script [down](down.sh) deallocates resources by stopping and removing the PostgreSQL container.

## Postman

A Postman [collection](postman) has been provided to test the API.
