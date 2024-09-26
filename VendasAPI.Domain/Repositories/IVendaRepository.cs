using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendasAPI.Domain.Entities;

namespace VendasAPI.Domain.Repositories
{
    public interface IVendaRepository
    {
        void Add(Venda venda);
        IEnumerable<Venda> GetAll();
        Venda GetById(int id);
        void Update(Venda venda);
    }
}
