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
            var venda = new Venda {};

            _vendaRepository.Add(venda);
            return venda;
        }

        public IEnumerable<Venda> GetAll() => _vendaRepository.GetAll();

        public Venda GetById(int id) => _vendaRepository.GetById(id);

        public void Update(int id, VendaDto vendaDto)
        {
            var venda = _vendaRepository.GetById(id);
            if (venda == null) return;
        }

        public void Cancel(int id)
        {
            var venda = _vendaRepository.GetById(id);
            if (venda == null) return;
        }
    }
}
