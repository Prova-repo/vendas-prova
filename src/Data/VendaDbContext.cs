using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class VendaDbContext : DbContext
{
    public VendaDbContext(DbContextOptions<VendaDbContext> options) : base(options) { }

    public DbSet<Venda> Vendas { get; set; }
    public DbSet<ItemVenda> ItensVenda { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Venda>().HasKey(v => v.Id);
        modelBuilder.Entity<ItemVenda>().HasKey(i => new { i.ProdutoId, i.ProdutoNome });
    }
}
