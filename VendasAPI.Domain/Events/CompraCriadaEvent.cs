using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasAPI.Domain.Events
{
    public class CompraCriadaEvent
    {
        public int VendaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public string ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
