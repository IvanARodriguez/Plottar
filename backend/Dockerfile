FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS base
WORKDIR /app
EXPOSE 5900

# Development Stage with Hot Reload
FROM base AS dev
COPY backend/Api/ .
WORKDIR "/app"
# Command to enable hot reload
CMD ["dotnet", "watch", "--project", "api.csproj"]
