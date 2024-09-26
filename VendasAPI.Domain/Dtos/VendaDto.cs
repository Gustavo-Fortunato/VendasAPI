namespace VendasAPI.Domain.Dtos
{
    public class VendaDto
    {
        public string ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public List<ItemVendaDto> Itens { get; set; }
        public decimal ValorTotal { get; set; }
        public bool Cancelado { get; set; }
    }

    public class ItemVendaDto
    {
        public string ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
