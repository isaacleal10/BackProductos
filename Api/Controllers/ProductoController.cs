using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ControladorProducto : ControllerBase
    {
        private readonly ServicioProducto _servicio;

        public ControladorProducto(ServicioProducto servicio) => _servicio = servicio;

        
        [HttpGet]
        [Authorize(Roles = "lectura,escritura")]
        public async Task<IEnumerable<Producto>> ObtenerTodos() => await _servicio.ObtenerTodos();

        
        [HttpGet("{id}")]
        [Authorize(Roles = "lecturaa,escrituraa")]
        public async Task<ActionResult<Producto>> ObtenerPorId(int id)
        {
            var producto = await _servicio.ObtenerPorId(id);
            return producto is null ? NotFound() : Ok(producto);
        }

        
        [HttpPost]
        [Authorize(Roles = "escritura")]
        public async Task<IActionResult> Agregar([FromBody] Producto producto)
        {
            await _servicio.Agregar(producto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, producto);
        }

        
        [HttpPut("{id}")]
        [Authorize(Roles = "escritura")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id) return BadRequest();
            await _servicio.Actualizar(producto);
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        [Authorize(Roles = "escritura")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _servicio.Eliminar(id);
            return NoContent();
        }
    }
}
