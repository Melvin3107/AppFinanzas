# Use the official .NET image from the Docker Hub
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8581

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Api/Usuarios/Usuarios.csproj", "Api/Usuarios/"]
RUN dotnet restore "Api/Usuarios/Usuarios.csproj"
COPY . .
WORKDIR "/src/Api/Usuarios"
RUN dotnet build "Usuarios.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Usuarios.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Usuarios.dll"]
