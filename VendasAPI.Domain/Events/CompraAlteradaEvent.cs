using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasAPI.Domain.Events
{
    public class CompraAlteradaEvent
    {
        public int VendaId { get; set; }
        public DateTime DataAlteracao { get; set; }
        public decimal NovoValorTotal { get; set; }
    }
}
