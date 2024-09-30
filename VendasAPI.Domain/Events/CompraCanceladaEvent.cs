using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasAPI.Domain.Events
{
    public class CompraCanceladaEvent
    {
        public int VendaId { get; set; }
        public DateTime DataCancelamento { get; set; }
    }
}
