using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sultana.API.Data;
using Sultana.Shared.Entities;

namespace Sultana.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaCabeceraController : ControllerBase
    {
        private readonly DataContext _context;

        public VentaCabeceraController(DataContext context)
        {
            _context = context;
        }

        // Metodo GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaCabecera>>> GetVentaCabeceras()
        {
            return await _context.VentaCabeceras.ToListAsync();
        }

       
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VentaCabecera>> GetVentaCabecera(int id)
        {
            var venta = await _context.VentaCabeceras.FindAsync(id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        // Metodo POST
        [HttpPost]
        public async Task<ActionResult<VentaCabecera>> PostVentaCabecera(VentaCabecera venta)
        {
            _context.VentaCabeceras.Add(venta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVentaCabecera), new { id = venta.Id }, venta);
        }
        
        // Metodo PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutVentaCabecera(int id, VentaCabecera venta)
        {
            if (id != venta.Id)
            {
                return BadRequest("El ID de la URL no coincide con el ID de la solicitud.");
            }

            _context.Entry(venta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaCabeceraExists(id))
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
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVentaCabecera(int id)
        {
            var venta = await _context.VentaCabeceras.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.VentaCabeceras.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentaCabeceraExists(int id)
        {
            return _context.VentaCabeceras.Any(e => e.Id == id);
        }
    }
}
