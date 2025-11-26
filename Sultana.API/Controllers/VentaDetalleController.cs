using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sultana.API.Data;
using Sultana.Shared.Entities;

namespace Sultana.Server.Controllers
{
    [ApiController]
    [Route("api/ventadetalle")]
    public class VentaDetalleController : ControllerBase
    {
        private readonly DataContext _context;

        public VentaDetalleController(DataContext context)
        {
            _context = context;
        }

        // Metodo GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaDetalle>>> GetVentaDetalles()
        {
            return await _context.VentaDetalles.ToListAsync();
        }

      
        [HttpGet("{id:long}")]
        public async Task<ActionResult<VentaDetalle>> GetVentaDetalle(long id)
        {
            var detalle = await _context.VentaDetalles.FindAsync(id);

            if (detalle == null)
            {
                return NotFound();
            }

            return Ok(detalle);
        }

        // Metodo POST
        [HttpPost]
        public async Task<ActionResult<VentaDetalle>> PostVentaDetalle(VentaDetalle ventaDetalle)
        {
            _context.VentaDetalles.Add(ventaDetalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVentaDetalle), new { id = ventaDetalle.Id }, ventaDetalle);
        }

        // Metodo PUT
        [HttpPut("{id:long}")]
        public async Task<IActionResult> PutVentaDetalle(long id, VentaDetalle ventaDetalle)
        {
            if (id != ventaDetalle.Id)
            {
                return BadRequest();
            }

            _context.Entry(ventaDetalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.VentaDetalles.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Metodo DELETE
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteVentaDetalle(long id)
        {
            var detalle = await _context.VentaDetalles.FindAsync(id);
            if (detalle == null)
            {
                return NotFound();
            }

            _context.VentaDetalles.Remove(detalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
