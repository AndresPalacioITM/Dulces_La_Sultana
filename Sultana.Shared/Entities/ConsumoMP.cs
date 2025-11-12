using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities;

public class ConsumoMP
{
    public long Id { get; set; }

    [Display(Name = "OP"), Required]
    public int OrdenProduccionId { get; set; }

    [Display(Name = "Lote MP"), Required]
    public int LoteMateriaPrimaId { get; set; }

    [Display(Name = "Responsable"), Required]
    public int ResponsableId { get; set; }
    [Display(Name = "Cantidad usada (kg)")]
    [Range(0, 1_000_000_000)]
    public decimal CantidadUsada { get; set; }

    [Display(Name = "Fecha"), Required]
    public DateTime FechaHora { get; set; } = DateTime.UtcNow;

    //public OrdenProduccion OrdenProduccion { get; set; } = null!;
    //public LoteMateriaPrima LoteMateriaPrima { get; set; } = null!;
    public Empleado Responsable { get; set; } = null!;

    
}
