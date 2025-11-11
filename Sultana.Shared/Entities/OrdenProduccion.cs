using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class OrdenProduccion
    {
        public int IdOP { get; set; }
        public DateTime FechaHoraInicio { get; set; } = DateTime.UtcNow;
        public int IdPT { get; set; }
        public string LoteProducto { get; set; } = null!;
        public DateTime? FechaVencimientoPT { get; set; }
        public decimal CantidadPeso { get; set; }          // kg objetivo
        public int ResponsableId { get; set; }
    }
}
