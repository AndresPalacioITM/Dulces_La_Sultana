using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sultana.API.Data;
using Sultana.Shared.Entities;

namespace Sultana.API.Controllers
{
    [ApiController]
    [Route("api/productoterminado")]
    public class ProductoTerminadoController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductoTerminadoController(DataContext context)
        {
            _context = context;
        }

        //Metodo GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoTerminado>>> GetProductosTerminados()
        {
            return Ok(await _context.ProductoTerminados.ToListAsync());
        }

      
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoTerminado>> GetProductoTerminado(int id)
        {
            var producto = await _context.ProductoTerminados.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // Metodo POST
        [HttpPost]
        public async Task<ActionResult<ProductoTerminado>> PostProductoTerminado(ProductoTerminado producto)
        {
            _context.ProductoTerminados.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductoTerminado), new { id = producto.Id }, producto);
        }

        //Metodo PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProductoTerminado(int id, ProductoTerminado producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ProductoTerminados.Any(e => e.Id == id))
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
        public async Task<IActionResult> DeleteProductoTerminado(int id)
        {
            var producto = await _context.ProductoTerminados.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.ProductoTerminados.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
