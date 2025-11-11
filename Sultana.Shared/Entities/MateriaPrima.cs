using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class MateriaPrima
    {
        public int Id { get; set; }

        [Display(Name = "Nombre Materia Prima")]
        [Required, MaxLength(120)]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Unidad")]
        [Required, MaxLength(10)] // 'kg','g','L','ml'
        public string UnidadMedida { get; set; } = "kg";

        [Display(Name = "Stock mínimo")]
        [Range(0, 999_999_999)]
        public decimal StockMinimo { get; set; } = 0;

        [JsonIgnore] public ICollection<LoteMateriaPrima> Lotes { get; set; } = new List<LoteMateriaPrima>();
    }
}
