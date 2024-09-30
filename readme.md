# Vendas API

Esta � uma API para o sistema de vendas de uma empresa fict�cia, desenvolvida com .NET 6 (ou superior), utilizando o Entity Framework Core para acesso a dados e Docker para containeriza��o. A API se conecta a um banco de dados SQL Server.

## Requisitos

Antes de iniciar, certifique-se de ter os seguintes requisitos instalados no seu ambiente de desenvolvimento:

- [.NET SDK 8.0 ou superior](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started) e Docker Compose
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou Docker para rodar o SQL Server como cont�iner)

### Clonando o Reposit�rio

```bash
git clone https://github.com/repo-teste-daniel/vendas
cd vendas-api
```


### Explica��es adicionais:
1. **Docker Compose**: O `docker-compose.yml` cuida da orquestra��o dos servi�os (API e SQL Server) para que eles subam e se conectem automaticamente.
2. **Gerenciamento de Migra��es**: O `dotnet ef` � usado tanto dentro quanto fora do Docker para gerar e aplicar migra��es.
3. **Estrutura do Projeto**: A estrutura do projeto mostra como os arquivos est�o organizados, especialmente o fato de que o `appsettings.json` est� no projeto **Api**.
