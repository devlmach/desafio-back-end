using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            await _usuarioService.AddAsync(usuario);
            return Ok(usuario);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsuarioById(int? id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
                return NotFound($"Usuario com id {id} não encontrado!");

            return Ok(usuario);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUsuario(int? id, [FromBody] Usuario usuarioExists)
        {
            if (id != usuarioExists.Id || usuarioExists == null)
                return BadRequest();

            await _usuarioService.UpdateAsync(usuarioExists);
            return Ok(usuarioExists);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUsuario(int? id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
                return NotFound();

            await _usuarioService.DeleteAsync(id);
            return Ok("Deleted Successfully");
        }
    }
}
