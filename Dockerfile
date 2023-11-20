# Use a common base image for both architectures
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base-amd64
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use a different base image for arm64v8 architecture
FROM mcr.microsoft.com/dotnet/aspnet:8.0.0-bookworm-slim-arm64v8 AS base-arm64v8
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["guran_2_2023.csproj", "."]
RUN dotnet restore "./././guran_2_2023.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./guran_2_2023.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./guran_2_2023.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the appropriate base image for each architecture
FROM base-amd64 AS final-amd64
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "guran_2_2023.dll"]

FROM base-arm64v8 AS final-arm64v8
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "guran_2_2023.dll"]

# Create a manifest to support multi-architecture
FROM scratch AS manifest
COPY --from=final-amd64 / /  
# This assumes final-amd64 is the last stage for amd64
COPY --from=final-arm64v8 / /  
# This assumes final-arm64v8 is the last stage for arm64v8
