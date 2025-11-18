using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Domain.Errors;
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

        /// <summary>
        /// Endpoint para criar um usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CreateUsuarioDto>> CreateUsuario([FromBody] CreateUsuarioDto usuario)
        {
            if (usuario == null)
                throw new BadRequestException("Invalid data.");

            await _usuarioService.AddAsync(usuario);
            return Ok(usuario);
        }

        /// <summary>
        /// Endpoint para retornar um usuário pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DetailUsuarioDto>> GetUsuarioById(int? id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
                throw new NotFoundException("User not found.");

            return Ok(usuario);
        }

        /// <summary>
        /// Endpoint para retornar uma lista de usuários
        /// </summary>
        /// <returns></returns>
        /// <response code="200"> lista Obtida. </response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetailUsuarioDto>>> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Endpoint para atualizar um usuário pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUsuarioDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUsuario(int? id, [FromBody] UpdateUsuarioDto updateUsuarioDto)
        {
            if (id != updateUsuarioDto.Id)
                throw new BadRequestException("Different id for operation.");

            if (updateUsuarioDto == null)
                throw new BadRequestException("Invalid data.");

            await _usuarioService.UpdateAsync(updateUsuarioDto);
            return Ok(updateUsuarioDto);
        }

        /// <summary>
        /// Endpoint para desativar um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetailUsuarioDto>> DeleteUsuario(long? id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
                throw new NotFoundException($"User with id {id} not found.");

            usuario.IsActive = false;
            await _usuarioService.DeleteAsync(id);
            return Ok(usuario);
        }
    }
}
