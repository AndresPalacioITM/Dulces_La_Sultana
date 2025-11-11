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

        // FKs
        public int IdOP { get; set; }
        public int IdLoteMP { get; set; }
        public int ResponsableId { get; set; }

        public decimal CantidadUsada { get; set; }      // en kg (si usas otras unidades, conviértelas antes)
        public DateTime FechaHora { get; set; } = DateTime.UtcNow;

        // Navegaciones
        public OrdenProduccion? OP { get; set; }
        public LoteMateriaPrima? LoteMP { get; set; }
        public Empleado? Responsable { get; set; }
    }
}
