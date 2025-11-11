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

        // FK
        public int IdPT { get; set; }
        public int ResponsableId { get; set; }

        // Datos de la OP
        public DateTime FechaHoraInicio { get; set; } = DateTime.UtcNow;
        public string LoteProducto { get; set; } = null!;
        public DateTime? FechaVencimientoPT { get; set; }
        public decimal CantidadPeso { get; set; }   // kg objetivo

        // Navegaciones
        public ProductoTerminado? Producto { get; set; }
        public Empleado? Responsable { get; set; }

        public LoteProductoTerminado? LotePT { get; set; }  // 1:1
        public ICollection<ConsumoMP> Consumos { get; set; } = new List<ConsumoMP>();
    }
}
