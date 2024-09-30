namespace Domain;

public class ItemVenda
{
    public Guid ProdutoId { get; set; } // External Identity
    public string ProdutoNome { get; set; } // Descritivo Desnormalizado
    public int Quantidade { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal Desconto { get; set; }
    public decimal ValorTotal => (ValorUnitario * Quantidade) - Desconto;
    public bool Cancelado { get; set; }

    public void CancelarItem()
    {
        Cancelado = true;
    }
}
