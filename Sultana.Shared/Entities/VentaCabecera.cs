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

        // FKs
        public int IdCliente { get; set; }
        public int ResponsableId { get; set; }

        public DateTime FechaHora { get; set; } = DateTime.UtcNow;
        public string Factura { get; set; } = null!;
        public string? Observaciones { get; set; }

        public Cliente? Cliente { get; set; }
        public Empleado? Responsable { get; set; }
        public ICollection<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
    }
}
