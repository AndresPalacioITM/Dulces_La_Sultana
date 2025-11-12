using Sultana.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class Alerta
    {
        public int Id { get; set; }
        public string ProductoId { get; set; } = null!;
        public TipoAlerta Tipo { get; set; }
        public string Mensaje { get; set; } = null!;
        public DateTime FechaGenerada { get; set; } = DateTime.UtcNow;
        public bool Notificada { get; set; } = false;

        public int ProveedorId { get; set; }
        public Proveedor? Proveedor { get; set; }
    }
}
