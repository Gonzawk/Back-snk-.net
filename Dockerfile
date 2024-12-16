# Usamos la imagen oficial de .Net sdk para compilar el proyecto 
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR apiweb
# Aplicamos un nombre a la aplicacion

# Exponemos los puertos 
EXPOSE 80 
EXPOSE 5201

# Copiamos los archivos del proyecto ]
COPY ./*.csproj ./
RUN dotnet restore

# Copiamos el resto de las cosas 
COPY . . 
RUN dotnet publish -c Release -o out

# Build image / Construir la imagen 
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /apiweb
COPY  --from=build /apiweb/out
ENTRYPOINT ["dotnet","Api-snk.dll"]