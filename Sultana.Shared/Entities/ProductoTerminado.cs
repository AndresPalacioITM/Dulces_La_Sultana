using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{

    public class ProductoTerminado
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        [Required, MaxLength(120)]
        public string Nombre { get; set; } = null!;

        [JsonIgnore] public ICollection<OrdenProduccion> Ordenes { get; set; } = new List<OrdenProduccion>();
    }
}
