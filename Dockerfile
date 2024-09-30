# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copiar o arquivo .csproj e restaurar dependências
COPY src/Domain/Domain.csproj ./src/Domain/
COPY src/Data/Data.csproj ./src/Data/
COPY src/Api/Api.csproj ./src/Api/

COPY *.sln ./
RUN dotnet restore ./src/Api/Api.csproj

# Copiar todo o código e compilar
COPY . .
WORKDIR /app/src/Api
RUN dotnet publish -c Release -o /app/out

# Etapa de produção
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

COPY ./scripts/wait-for-it.sh /app/scripts/wait-for-it.sh
RUN chmod +x /app/scripts/wait-for-it.sh


ENTRYPOINT ["dotnet", "Api.dll"]
