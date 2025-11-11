using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sultana.Shared.Entities
{
    public class VentaDetalle
    {
        public long IdDetVenta { get; set; }
        public int IdVenta { get; set; }
        public int IdLotePT { get; set; }
        public decimal Cantidad { get; set; }
    }
}
