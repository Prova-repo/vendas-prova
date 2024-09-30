using Data;
using Microsoft.EntityFrameworkCore;
using Domain.Repositorios.Interfaces;

namespace Domain.Repositorios;

public class VendaRepository : IVendaRepository
{
    private readonly VendaDbContext _context;

    public VendaRepository(VendaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Venda venda)
    {
        await _context.Vendas.AddAsync(venda);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Venda venda)
    {
        _context.Vendas.Update(venda);

        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Guid id)
    {
        var venda = await ObterPorIdAsync(id);
        _context.Vendas.Remove(venda);

        await _context.SaveChangesAsync();
    }

    public async Task<Venda> ObterPorIdAsync(Guid id)
        => await _context.Vendas.Include(v => v.Itens).FirstOrDefaultAsync(v => v.Id == id);

    public async Task<List<Venda>> ObterTodasAsync()
        => await _context.Vendas.Include(v => v.Itens).ToListAsync();
}
