using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sultana.API.Data;
using Sultana.Shared.Entities;

namespace Sultana.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly DataContext _context;

        public EmpleadoController(DataContext context)
        {
            _context = context;
        }

        // Metodo GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados()
        {
            return await _context.Empleados.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await _context.Empleados.FirstOrDefaultAsync(e => e.Id == id);

            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        // Metodo POST
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
        {
            // Validación para evitar nombres duplicados (por el índice único en tu DataContext)
            var existe = await _context.Empleados.AnyAsync(e => e.Nombre == empleado.Nombre);
            if (existe)
            {
                return BadRequest($"Ya existe un empleado con el nombre '{empleado.Nombre}'.");
            }

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return Ok(empleado);
        }

        // Metodo PUT
        [HttpPut]
        public async Task<ActionResult<Empleado>> PutEmpleado(Empleado empleado)
        {
            // Verifica que el empleado exista
            var existe = await _context.Empleados.AnyAsync(e => e.Id == empleado.Id);
            if (!existe)
            {
                return NotFound($"No se encontró el empleado con Id {empleado.Id}.");
            }

            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();

            return Ok(empleado);
        }

        // Meto DELETE
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteEmpleado(int id)
        {
            var filasAfectadas = await _context.Empleados
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();

            if (filasAfectadas == 0)
            {
                return NotFound();
            }

            return NoContent(); // 204
        }
    }
}
