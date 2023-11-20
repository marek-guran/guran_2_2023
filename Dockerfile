# See https://aka.ms/customizecontainer to learn how to customize your debug container
# and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Base image for ARM64
FROM mcr.microsoft.com/dotnet/aspnet:8.0.0-bookworm-slim-arm64v8 AS base-arm64
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Base image for AMD64
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base-amd64
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["guran_2_2023.csproj", "."]
RUN dotnet restore "./././guran_2_2023.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./guran_2_2023.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./guran_2_2023.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage for ARM64
FROM base-arm64 AS final-arm64
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "guran_2_2023.dll"]

# Final stage for AMD64
FROM base-amd64 AS final-amd64
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "guran_2_2023.dll"]
