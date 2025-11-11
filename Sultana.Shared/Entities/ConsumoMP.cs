using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class ConsumoMP
    {
        public long IdConsumoMP { get; set; }
        public int IdOP { get; set; }
        public int IdLoteMP { get; set; }
        public decimal CantidadUsada { get; set; }         // en kg
        public DateTime FechaHora { get; set; } = DateTime.UtcNow;
        public int ResponsableId { get; set; }
    }
}
