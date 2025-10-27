# ---- Build Stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore

# Build the project in Release mode
RUN dotnet publish -c Release -o out

# ---- Runtime Stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published output
COPY --from=build /app/out .

# Expose API port (change 8080 if your app uses a different port)
EXPOSE 8080

# Start the app
ENTRYPOINT ["dotnet", "BackEnd.dll"]
