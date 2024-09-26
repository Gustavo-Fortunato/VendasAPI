using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VendasAPI.Domain.Entities{

public class Venda
{
    public int VendaId { get; set; }
    public DateTime DataVenda { get; set; }
    public string ClienteId { get; set; }
    public string ClienteNome { get; set; }
    public decimal ValorTotal { get; set; }
    public bool Cancelado { get; set; }
    public List<ItemVenda> ItemVenda { get; set; }
}
    public class ItemVenda
{
        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
