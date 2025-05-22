# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Unab.Practice.Employee.WebApi/Unab.Practice.Employee.WebApi.csproj", "Unab.Practice.Employee.WebApi/"]
COPY ["Unab.Practice.Employee.Persistence/Unab.Practice.Employee.Persistence.csproj", "Unab.Practice.Employee.Persistence/"]
COPY ["Unab.Practice.Employee.Interfaces/Unab.Practice.Employee.Interfaces.csproj", "Unab.Practice.Employee.Interfaces/"]
COPY ["Unab.Practice.Employee.Domain/Unab.Practice.Employee.Domain.csproj", "Unab.Practice.Employee.Domain/"]
COPY ["Unab.Practice.Employee.Transversal/Unab.Practice.Employee.Transversal.csproj", "Unab.Practice.Employee.Transversal/"]
COPY ["Unab.Practice.Employee.UseCases/Unab.Practice.Employee.UseCases.csproj", "Unab.Practice.Employee.UseCases/"]
COPY ["Unab.Practice.Employee.Dto/Unab.Practice.Employee.Dto.csproj", "Unab.Practice.Employee.Dto/"]
RUN dotnet restore "./Unab.Practice.Employee.WebApi/Unab.Practice.Employee.WebApi.csproj"
COPY . .
WORKDIR "/src/Unab.Practice.Employee.WebApi"
RUN dotnet build "./Unab.Practice.Employee.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Unab.Practice.Employee.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unab.Practice.Employee.WebApi.dll"]