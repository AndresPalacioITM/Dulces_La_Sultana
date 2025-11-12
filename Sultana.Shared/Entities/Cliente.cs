using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities;

public class Cliente
{
    public int Id { get; set; }

    [Display(Name = "NIT")]
    [Required, MaxLength(30)]
    public string Nit { get; set; } = null!;

    [Display(Name = "Nombre")]
    [Required, MaxLength(120)]
    public string Nombre { get; set; } = null!;

    [Display(Name = "Empresa"), MaxLength(120)]
    public string? Empresa { get; set; }

    [Display(Name = "Dirección"), MaxLength(150)]
    public string? Direccion { get; set; }

    [Display(Name = "Contacto"), MaxLength(60)]
    public string? Contacto { get; set; }



    //[JsonIgnore] 
    //public ICollection<VentaCabecera> Ventas { get; set; } = new List<VentaCabecera>();
}
