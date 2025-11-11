using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class VentaCabecera
    {
        public int IdVenta { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.UtcNow;
        public int IdCliente { get; set; }
        public string Factura { get; set; } = null!;
        public int ResponsableId { get; set; }
        public string? Observaciones { get; set; }
    }
}
