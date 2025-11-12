using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sultana.API.Data;
using Sultana.Shared.Entities;

namespace Sultana.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertasController : ControllerBase
    {
        private readonly DataContext _context;

        public AlertasController(DataContext context)
        {
            _context = context;
        }

        //Get: api/Alertas/{proveedorID}
        [HttpGet("proveedor/{proveedorId}")]
        public async Task<ActionResult<IEnumerable<Alerta>>> GetAlertasProveedor(int proveedorId)
        { 
            var alertas = await _context.Alertas
                .Where(a => a.ProveedorId == proveedorId)
                .OrderByDescending(a => a.FechaGenerada)
                .ToListAsync();

            return alertas;
        }
    }
}
