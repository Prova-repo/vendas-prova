# Vendas API

Esta é uma API para o sistema de vendas de uma empresa fictícia, desenvolvida com .NET 6 (ou superior), utilizando o Entity Framework Core para acesso a dados e Docker para containerização. A API se conecta a um banco de dados SQL Server.

## Requisitos

Antes de iniciar, certifique-se de ter os seguintes requisitos instalados no seu ambiente de desenvolvimento:

- [.NET SDK 8.0 ou superior](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started) e Docker Compose
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou Docker para rodar o SQL Server como contêiner)

### Clonando o Repositório

```bash
git clone https://github.com/repo-teste-daniel/vendas
cd vendas-api
```


### Explicações adicionais:
1. **Docker Compose**: O `docker-compose.yml` cuida da orquestração dos serviços (API e SQL Server) para que eles subam e se conectem automaticamente.
2. **Gerenciamento de Migrações**: O `dotnet ef` é usado tanto dentro quanto fora do Docker para gerar e aplicar migrações.
3. **Estrutura do Projeto**: A estrutura do projeto mostra como os arquivos estão organizados, especialmente o fato de que o `appsettings.json` está no projeto **Api**.
