using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities;


public class Empleado
{
    public int Id { get; set; }

    [Display(Name = "Nombre del empleado")]
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; }

    [Display(Name = "Cargo")]
    [Required, MaxLength(80)]
    public string Cargo { get; set; }

    [Display(Name = "Contacto"), MaxLength(60)]
    public string? Contacto { get; set; }

    [Display(Name = "Correo"), EmailAddress, MaxLength(120)]
    public string? Email { get; set; }

    //[JsonIgnore] public ICollection<LoteMateriaPrima> LotesRecibidos { get; set; } = new List<LoteMateriaPrima>();
    //[JsonIgnore] public ICollection<OrdenProduccion> Ordenes { get; set; } = new List<OrdenProduccion>();
    //[JsonIgnore] public ICollection<ConsumoMP> Consumos { get; set; } = new List<ConsumoMP>();
    //[JsonIgnore] public ICollection<VentaCabecera> VentasAtendidas { get; set; } = new List<VentaCabecera>();
}
