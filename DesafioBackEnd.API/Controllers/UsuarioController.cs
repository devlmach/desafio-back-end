using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Common.Middleware;
using DesafioBackEnd.API.Domain.Entity;
using DesafioBackEnd.API.Domain.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(DetailUsuarioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
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
        [Authorize(Roles = "Admin, User")]
        [ProducesResponseType(typeof(DetailUsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<DetailUsuarioDto>> GetUsuarioById(long? id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
           
            if (User.IsInRole(UserRole.Admin.ToString()))
            {
                if(usuario == null)
                {
                    throw new NotFoundException("User not found.");
                }
                return Ok(usuario);
            }

            if (User.IsInRole(UserRole.User.ToString()))
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if (usuario == null || usuario.Email != userEmail)
                {
                    throw new ForbiddenException("User typerole User can only see your own details.");
                }
                else
                {
                    return Ok(usuario);
                }               
            }

            return Ok(usuario);
        }

        /// <summary>
        /// Endpoint para retornar uma lista de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(DetailUsuarioDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DetailUsuarioDto>>> GetAllUsuarios([FromQuery] QueryUsuarioParameter queryParameter)
        {
            var usuarios = await _usuarioService.GetUsuariosAsync(queryParameter.NomeCompleto, queryParameter.Cpf, queryParameter.Email, queryParameter.Tipo, queryParameter.Role, queryParameter.IsActive, queryParameter.PageNumber,
                queryParameter.PageSize);

            return Ok(usuarios);
        }

        /// <summary>
        /// Endpoint para atualizar um usuário pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUsuarioDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, User")]
        [ProducesResponseType(typeof(UpdateUsuarioDto), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateUsuario(long? id, [FromBody] UpdateUsuarioDto updateUsuarioDto)
        {
            if (id != updateUsuarioDto.Id)
                throw new BadRequestException("Different id for operation.");

            if (updateUsuarioDto == null)
                throw new BadRequestException("Invalid data.");

            if (User.IsInRole(UserRole.User.ToString()))
            {
                var emailUser = User.FindFirstValue(ClaimTypes.Email);
                if (updateUsuarioDto.Email != emailUser)
                {
                    throw new ForbiddenException("User typerole User cannot update other users details.");
                }
            }

            await _usuarioService.UpdateAsync(updateUsuarioDto);
            return Ok(updateUsuarioDto);
        }

        /// <summary>
        /// Endpoint para desativar um usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DetailUsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
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
