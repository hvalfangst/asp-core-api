FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder

WORKDIR /app

COPY . ./

RUN dotnet restore Api.csproj

RUN dotnet publish Api.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime

WORKDIR /app

COPY --from=builder /app/out .

# Default port exposed in Dotnet 8 containers
EXPOSE 8080

ENTRYPOINT ["dotnet", "Api.dll"]