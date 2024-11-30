#!/bin/sh

# Exits immediately if a command exits with a non-zero status
set -e

# Run 'docker-compose up' for creating our DB container
docker-compose -f infra/db/heroes/docker-compose.yml up -d

# Start the application
dotnet run --project HvalfangstApi/HvalfangstApi.csproj