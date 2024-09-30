namespace Domain;

public class Venda
{
    public Guid Id { get; set; }
    public string NumeroVenda { get; set; }
    public DateTime DataVenda { get; set; }
    public Guid ClienteId { get; set; }
    public string ClienteNome { get; set; }
    public decimal ValorTotal { get; set; }
    public Guid FilialId { get; set; }
    public string FilialNome { get; set; }
    public List<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
    public bool Cancelado { get; set; }

    public void CancelarVenda()
    {
        Cancelado = true;
        foreach (var item in Itens)
        {
            item.CancelarItem();
        }
    }
}
