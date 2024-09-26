using VendasAPI.Domain.Dtos;
using VendasAPI.Domain.Entities;
using VendasAPI.Domain.Repositories;

namespace VendasAPI.Domain.Services
{
    public class VendaService(IVendaRepository vendaRepository) : IVendaService
    {
        private readonly IVendaRepository _vendaRepository = vendaRepository;

        public Venda Create(VendaDto vendaDto)
        {
            var venda = new Venda
            {
                ClienteId = vendaDto.ClienteId,
                ClienteNome = vendaDto.ClienteNome,
                ItemVenda = vendaDto.Itens.Select(i => new ItemVenda
                {
                    ProdutoId = i.ProdutoId,
                    ProdutoNome = i.ProdutoNome,
                    Quantidade = i.Quantidade,
                    ValorUnitario = i.ValorUnitario,
                    Desconto = i.Desconto,
                    ValorTotal = i.ValorTotal
                }).ToList(),
                ValorTotal = vendaDto.Itens.Sum(i => i.ValorTotal),
                Cancelado = false,
                DataVenda = DateTime.UtcNow
            };

            _vendaRepository.Add(venda);
            return venda;
        }

        public IEnumerable<Venda> GetAll() => _vendaRepository.GetAll();

        public Venda GetById(int id) => _vendaRepository.GetById(id);

        public void Update(int id, VendaDto vendaDto)
        {
            var venda = _vendaRepository.GetById(id);
            if (venda == null) return;

            venda.ClienteId = vendaDto.ClienteId;
            venda.ClienteNome = vendaDto.ClienteNome;
            venda.ItemVenda = vendaDto.Itens.Select(i => new ItemVenda
            {
                ProdutoId = i.ProdutoId,
                ProdutoNome = i.ProdutoNome,
                Quantidade = i.Quantidade,
                ValorUnitario = i.ValorUnitario,
                Desconto = i.Desconto,
                ValorTotal = i.ValorTotal
            }).ToList();
            venda.ValorTotal = vendaDto.Itens.Sum(i => i.ValorTotal);

            _vendaRepository.Update(venda);
        }

        public void Cancel(int id)
        {
            var venda = _vendaRepository.GetById(id);
            if (venda == null) return;

            venda.Cancelado = true;
            _vendaRepository.Update(venda);
        }
    }
}
