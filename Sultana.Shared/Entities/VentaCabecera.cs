using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities;

public class VentaCabecera
{
    public int Id { get; set; }

    [Display(Name = "Fecha"), Required]
    public DateTime FechaHora { get; set; } = DateTime.UtcNow;

    [Display(Name = "Cliente"), Required]
    public int ClienteId { get; set; }

    [Display(Name = "Responsable"), Required]
    public int ResponsableId { get; set; }       

    [Display(Name = "Factura"), Required, MaxLength(60)]
    public string Factura { get; set; } = null!;

    [Display(Name = "Observaciones"), MaxLength(250)]
    public string? Observaciones { get; set; }


    //public Cliente Cliente { get; set; } = null!;
    //public Empleado Responsable { get; set; } = null!;
    //[JsonIgnore] public ICollection<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
}
