using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class LoteProductoTerminado
    {
        public int IdLotePT { get; set; }
        public int IdOP { get; set; }                   // 1:1 con OP
        public decimal StockPT { get; set; }            // stock actual del lote (kg o unidades)
        public DateTime? FechaVencimientoPT { get; set; }

        public OrdenProduccion? OP { get; set; }
        public ICollection<VentaDetalle> Ventas { get; set; } = new List<VentaDetalle>();
    }
}
