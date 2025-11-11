using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{

    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; } = null!;   // Único por negocio
        public string Cargo { get; set; } = null!;
        public string? Contacto { get; set; }
        public string? Email { get; set; }

    }
}
