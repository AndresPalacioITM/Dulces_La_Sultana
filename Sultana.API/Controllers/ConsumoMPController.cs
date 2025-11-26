using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sultana.API.Data;
using Sultana.Shared.Entities;

namespace Sultana.API.Controllers
{
    [Route("/api/consumomp")]
    [ApiController]
    public class ConsumoMPController : ControllerBase
    {
        private readonly DataContext _context;

        public ConsumoMPController(DataContext context)
        {
            _context = context;
        }

        // Metodo GET
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.ConsumoMPs.ToListAsync());
        }

        // GET: /api/consumomp/5
        [HttpGet("{id:long}")]
        public async Task<ActionResult> Get(long id)
        {
            var consumo = await _context.ConsumoMPs
                .FirstOrDefaultAsync(x => x.Id == id);

            if (consumo == null)
            {
                return NotFound();
            }

            return Ok(consumo);
        }

        // POST: /api/consumomp
        [HttpPost]
        public async Task<ActionResult<ConsumoMP>> Post(ConsumoMP consumoMP)
        {
            try 
            {
                var lote = await _context.LoteMateriaPrimas.FindAsync(consumoMP.LoteMateriaPrimaId);
                if (lote == null) return BadRequest("Lote de materia prima no encontrado.");
                var empleado = await _context.Empleados.FindAsync(consumoMP.ResponsableId);
                if (empleado == null) return BadRequest("Empleado responsable no encontrado.");
                var orden = await _context.OrdenProducciones.FindAsync(consumoMP.OrdenProduccionId);
                if (orden == null) return BadRequest("Orden de producción no encontrada.");
                if (lote.CantidadDisponible < consumoMP.CantidadUsada) return BadRequest("Cantidad usada excede la cantidad disponible en el lote.");

                lote.CantidadDisponible -= consumoMP.CantidadUsada;
                _context.ConsumoMPs.Add(consumoMP);
                await _context.SaveChangesAsync();

                return Ok(consumoMP); // 200

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }            
        }

        // PUT: /api/consumomp
        // PUT: /api/consumomp/5
        [HttpPut("{id:long}")]
        public async Task<ActionResult> Put(long id, ConsumoMP consumo)
        {
            try
            {
                if (id != consumo.Id)
                    return BadRequest("El ID de la ruta no coincide con el ID del consumo"); // 400

                // Verificar si el consumo existe
                var consumoExistente = await _context.ConsumoMPs
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (consumoExistente == null)
                    return NotFound("Consumo no encontrado"); // 404

                // Verificar referencias (similar al POST)
                var lote = await _context.LoteMateriaPrimas.FindAsync(consumo.LoteMateriaPrimaId);
                if (lote == null)
                    return BadRequest("Lote de materia prima no encontrado.");

                var empleado = await _context.Empleados.FindAsync(consumo.ResponsableId);
                if (empleado == null)
                    return BadRequest("Empleado responsable no encontrado.");

                var orden = await _context.OrdenProducciones.FindAsync(consumo.OrdenProduccionId);
                if (orden == null)
                    return BadRequest("Orden de producción no encontrada.");

                // Calcular la diferencia de cantidad para ajustar el lote
                var diferenciaCantidad = consumo.CantidadUsada - consumoExistente.CantidadUsada;

                if (lote.CantidadDisponible < diferenciaCantidad)
                    return BadRequest("La cantidad actualizada excede la cantidad disponible en el lote.");

                // Actualizar la cantidad disponible del lote
                lote.CantidadDisponible -= diferenciaCantidad;

                // Actualizar las propiedades del consumo existente
                consumoExistente.OrdenProduccionId = consumo.OrdenProduccionId;
                consumoExistente.LoteMateriaPrimaId = consumo.LoteMateriaPrimaId;
                consumoExistente.ResponsableId = consumo.ResponsableId;
                consumoExistente.CantidadUsada = consumo.CantidadUsada;
                consumoExistente.FechaHora = consumo.FechaHora;

                await _context.SaveChangesAsync();

                return Ok(consumoExistente); // 200
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ConsumoExists(id))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        private async Task<bool> ConsumoExists(long id)
        {
            return await _context.ConsumoMPs.AnyAsync(x => x.Id == id);
        }


        // DELETE: /api/consumomp/5
        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete(long id)
        {
            var filas = await _context.ConsumoMPs
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

            if (filas == 0)
                return NotFound(); // 404

            return NoContent(); // 204
        }
    }
}
