using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class LoteProductoTerminado
    {
        public int Id { get; set; }

        [Display(Name = "OP"), Required]
        public int OrdenProduccionId { get; set; }       

        [Display(Name = "Stock PT")]
        [Range(0, 1_000_000_000)]
        public decimal StockPT { get; set; }

        [Display(Name = "Vence PT")]
        public DateTime? FechaVencimientoPT { get; set; }


        public OrdenProduccion OrdenProduccion { get; set; } = null!;
        //[JsonIgnore] public ICollection<VentaDetalle> Ventas { get; set; } = new List<VentaDetalle>();
    }
}
