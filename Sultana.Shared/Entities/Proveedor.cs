using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities;

public class Proveedor
{
    public int Id { get; set; }

    [Display(Name = "NIT")]
    [Required, MaxLength(30)]
    public string Nit { get; set; } = null!;

    [Display(Name = "Nombre del vendedor")]
    [Required, MaxLength(120)]
    public string NombreVendedor { get; set; } = null!;

    [Display(Name = "Empresa")]
    [Required, MaxLength(120)]
    public string Empresa { get; set; } = null!;

    [Display(Name = "Contacto"), MaxLength(60)]
    public string? Contacto { get; set; }

    [Display(Name = "Dirección"), MaxLength(150)]
    public string? Direccion { get; set; }


    //[JsonIgnore] public ICollection<LoteMateriaPrima> LotesSuministrados { get; set; } = new List<LoteMateriaPrima>();
}

