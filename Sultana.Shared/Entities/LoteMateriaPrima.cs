using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sultana.Shared.Entities;

public class LoteMateriaPrima
{
    public int Id { get; set; }

    [Display(Name = "Materia prima"), Required]
    public int MateriaPrimaId { get; set; }

    [Display(Name = "Proveedor"), Required]
    public int ProveedorId { get; set; }

    [Display(Name = "Responsable"), Required]
    public int ResponsableId { get; set; }     

    [Display(Name = "N° Lote"), Required, MaxLength(60)]
    public string NumeroLote { get; set; } = null!;

    [Display(Name = "Recepción"), Required]
    public DateTime FechaHoraRecepcion { get; set; } = DateTime.UtcNow;

    [Display(Name = "Vence")]
    public DateTime? FechaVencimiento { get; set; }

    [Display(Name = "Cantidad ingresada")]
    [Range(0, 1_000_000_000)]
    public decimal CantidadIngresada { get; set; }

    [Display(Name = "Cantidad disponible")]
    [Range(0, 1_000_000_000)]
    public decimal CantidadDisponible { get; set; }

    [Display(Name = "Color"), Required, MaxLength(10)]
    public string InspeccionColor { get; set; } = "CUMPLE";

    [Display(Name = "Olor"), Required, MaxLength(10)]
    public string InspeccionOlor { get; set; } = "CUMPLE";

    [Display(Name = "Textura"), Required, MaxLength(10)]
    public string InspeccionTextura { get; set; } = "CUMPLE";

    [Display(Name = "Empaque"), Required, MaxLength(10)]
    public string EstadoEmpaque { get; set; } = "CUMPLE";

    [Display(Name = "Observaciones"), MaxLength(250)]
    public string? Observaciones { get; set; }

    [JsonIgnore]
    public MateriaPrima MateriaPrima { get; set; } = null!;
    [JsonIgnore]
    public Proveedor Proveedor { get; set; } = null!;
    [JsonIgnore]
    public Empleado Responsable { get; set; } = null!;

    //[JsonIgnore]
    //public ICollection<ConsumoMP> Consumos { get; set; } = new List<ConsumoMP>();
}
