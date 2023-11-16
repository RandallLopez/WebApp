using AplicacionWeb.Models;
using AplicacionWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace AplicacionWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LugarEventoController : ControllerBase
    {
        private readonly ILugarEventoService _service;

        public LugarEventoController(ILugarEventoService service)
        {
            _service = service;
        }

        // Obtener todos los Lugares de eventos
        [HttpGet]
        [Route("Obtener")]
        public async Task<IActionResult> Obtener()
        {
            var data = await _service.Obtener();
            return StatusCode(StatusCodes.Status200OK, data);
        }

        // Ver un Lugar de evento
        [HttpGet]
        [Route("Ver/{id:int}")]
        public async Task<IActionResult> Ver(int id)
        {
            var lugarEvento = await _service.GetByIdAsync(id);
            return StatusCode(StatusCodes.Status200OK, lugarEvento);
        }

        // Agregar un Lugar de evento
        [HttpPost]
        [Route("Agregar")]
        public async Task<IActionResult> Agregar([FromBody] LugarEvento request)
        {
            await _service.AddAsync(request);
            return StatusCode(StatusCodes.Status200OK, "Ok");
        }

        // Editar un Lugar de evento
        [HttpPut]
        [Route("Editar/{id:int}")]
        public async Task<IActionResult> Editar([FromBody] LugarEvento request, int id)
        {
            await _service.UpdateAsync(id, request);
            return StatusCode(StatusCodes.Status200OK, "Ok");
        }

        // Eliminar un Lugar de evento
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _service.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK, "Ok");
        }

    }
}