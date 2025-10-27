# ---- Build Stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all files and restore dependencies
COPY . ./
RUN dotnet restore

# Build the terminal frontend
RUN dotnet publish -c Release -o /app/out

# ---- Runtime Stage ----
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app

# Copy build output
COPY --from=build /app/out .

# Run the terminal app
ENTRYPOINT ["dotnet", "FrontEnd.dll"]
