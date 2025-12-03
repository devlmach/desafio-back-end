using DesafioBackEnd.API.Application.Command.Usuarios;

namespace DesafioBackEnd.API.Application.Dto.Usuarios
{
    public class UpdateUsuarioDto : UsuarioCommand
    {
        public long Id { get; set; }
    }
}
