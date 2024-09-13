# Usa una imagen base con .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Instala las herramientas necesarias
RUN apt-get update && apt-get install -y \
    wget \
    unzip \
    # Añadir cualquier otra herramienta necesaria aquí

# Descarga y descomprime ConfuserEx
RUN wget https://github.com/microsoft/ConfuserEx/releases/download/v1.0.0/ConfuserEx_1.0.0.zip \
    && unzip ConfuserEx_1.0.0.zip -d /opt/confuserex \
    && rm ConfuserEx_1.0.0.zip

# Establece el directorio de trabajo
WORKDIR /app

# Copia el proyecto y archivos necesarios al contenedor
COPY . /app

# Compila la aplicación (ajusta según tu proyecto)
RUN dotnet build YourProject.sln -c Release

# Ejecuta ConfuserEx para obfuscar la aplicación
# Ajusta el comando según la forma en que configures ConfuserEx
# y asegúrate de tener el archivo de configuración correcto
RUN dotnet /opt/confuserex/Confuser.CLI.dll -c ConfuserConfig.crproj

# Comando predeterminado (opcional) para ejecutar la aplicación obfuscada
# CMD ["dotnet", "path/to/your/obfuscated/app.dll"]

