using DesafioBackEnd.API.Application.Command.Usuarios;
using DesafioBackEnd.API.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Application.Dto.Usuarios
{
    public class UpdateUsuarioDto : UsuarioCommand
    {
        public long Id { get; set; }
    }
}
