using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasAPI.Domain.Dtos;
using VendasAPI.Domain.Entities;

namespace VendasAPI.Domain.Services
{
    public interface IVendaService
    {
        Venda Create(VendaDto vendaDto);
        IEnumerable<Venda> GetAll();
        Venda GetById(int id);
        void Update(int id, VendaDto vendaDto);
        void Cancel(int id);
    }
}
