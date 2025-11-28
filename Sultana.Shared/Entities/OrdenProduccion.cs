using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities;

public class OrdenProduccion
{
    public int Id { get; set; }

    [Display(Name = "Inicio"), Required]
    public DateTime FechaHoraInicio { get; set; } = DateTime.UtcNow;

    [Display(Name = "Producto"), Required]
    public int ProductoTerminadoId { get; set; }

    [Display(Name = "Responsable"), Required]
    public int ResponsableId { get; set; }        

    [Display(Name = "Lote PT"), Required, MaxLength(60)]
    public string LoteProducto { get; set; } = null!;

    [Display(Name = "Vence PT")]
    public DateTime? FechaVencimientoPT { get; set; }

    [Display(Name = "Cantidad objetivo (kg)")]
    [Range(0, 1_000_000_000)]
    public decimal CantidadPeso { get; set; }


    [JsonIgnore]
    public ProductoTerminado ProductoTerminado { get; set; } = null!;

    public Empleado Responsable { get; set; } = null!;
    [JsonIgnore]
    public LoteProductoTerminado? LotePT { get; set; }
    //[JsonIgnore] public ICollection<ConsumoMP> Consumos { get; set; } = new List<ConsumoMP>();
}
