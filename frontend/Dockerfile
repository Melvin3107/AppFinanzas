# Use the official .NET image from the Docker Hub
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8585

# Usa la imagen oficial de .NET SDK para construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo csproj y el archivo nuget.config a la carpeta de trabajo

COPY ["frontend/frontend.csproj", "frontend/"]
RUN dotnet restore "frontend/frontend.csproj"
COPY . .
WORKDIR "/src/frontend"
RUN dotnet build "frontend.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "frontend.csproj" -c Release -o /app/publish

# Usa la imagen base para el contenedor final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY frontend/wwwroot /app/wwwroot

ENTRYPOINT ["dotnet", "frontend.dll"]
