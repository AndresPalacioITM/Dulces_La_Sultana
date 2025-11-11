using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string Nit { get; set; } = null!;
        public string NombreVendedor { get; set; } = null!;
        public string Empresa { get; set; } = null!;
        public string? Contacto { get; set; }
        public string? Direccion { get; set; }
    }
}
}
