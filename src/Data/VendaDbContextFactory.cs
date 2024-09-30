using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace Data;

public class VendaDbContextFactory : IDesignTimeDbContextFactory<VendaDbContext>
{
    public VendaDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<VendaDbContext>();

        // Caminho do projeto da API, onde o appsettings.json está
        var apiProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Api");

        // Criação do ConfigurationBuilder, apontando para o appsettings.json no projeto Api
        var configuration = new ConfigurationBuilder()
            .SetBasePath(apiProjectPath) // Define o caminho base para o projeto da API
            .AddJsonFile("appsettings.json") // Carrega o arquivo de configuração
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);

        return new VendaDbContext(optionsBuilder.Options);
    }
}
