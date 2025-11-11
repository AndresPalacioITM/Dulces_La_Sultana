using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class VentaDetalle
    {
        public long Id { get; set; }

        [Display(Name = "Venta"), Required]
        public int VentaCabeceraId { get; set; }

        [Display(Name = "Lote PT"), Required]
        public int LoteProductoTerminadoId { get; set; }        

        [Display(Name = "Cantidad")]
        [Range(0, 1_000_000_000)]
        public decimal Cantidad { get; set; }

        //public VentaCabecera VentaCabecera { get; set; } = null!;
        //public LoteProductoTerminado LoteProductoTerminado { get; set; } = null!;
    }
}
