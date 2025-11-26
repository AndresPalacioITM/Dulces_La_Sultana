using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sultana.API.Data;
using Sultana.Shared.Entities;

namespace Sultana.API.Controllers
{
    [ApiController]
    [Route("api/ordenproduccion")]
    public class OrdenProduccionController : ControllerBase
    {
        private readonly DataContext _context;

        public OrdenProduccionController(DataContext context)
        {
            _context = context;
        }

        // Metodo GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenProduccion>>> GetOrdenesProduccion()
        {
            var ordenes = await _context.OrdenProducciones
                .Include(o => o.ProductoTerminado)
                .Include(o => o.Responsable)
                .ToListAsync();

            return Ok(ordenes);
        }

       
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrdenProduccion>> GetOrdenProduccion(int id)
        {
            var orden = await _context.OrdenProducciones
                .Include(o => o.ProductoTerminado)
                .Include(o => o.Responsable)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (orden == null)
            {
                return NotFound();
            }

            return Ok(orden);
        }

        // Metodo POST
        [HttpPost]
        public async Task<ActionResult<OrdenProduccion>> PostOrdenProduccion(OrdenProduccion orden)
        {
            _context.OrdenProducciones.Add(orden);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrdenProduccion), new { id = orden.Id }, orden);
        }

        // Metodo PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutOrdenProduccion(int id, OrdenProduccion orden)
        {
            if (id != orden.Id)
            {
                return BadRequest();
            }

            _context.Entry(orden).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.OrdenProducciones.Any(e => e.Id == id))
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

        //Metodo DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrdenProduccion(int id)
        {
            var orden = await _context.OrdenProducciones.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }

            _context.OrdenProducciones.Remove(orden);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
