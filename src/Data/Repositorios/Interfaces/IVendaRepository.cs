namespace Domain.Repositorios.Interfaces;

public interface IVendaRepository
{
    Task<Venda> ObterPorIdAsync(Guid id);
    Task<List<Venda>> ObterTodasAsync();
    Task AdicionarAsync(Venda venda);
    Task AtualizarAsync(Venda venda);
    Task ExcluirAsync(Guid id);
}
