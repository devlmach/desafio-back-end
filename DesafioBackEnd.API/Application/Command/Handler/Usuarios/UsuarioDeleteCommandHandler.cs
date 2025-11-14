using DesafioBackEnd.API.Application.Command.Usuarios;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Handler.Usuarios
{
    public class UsuarioDeleteCommandHandler : IRequestHandler<UsuarioDeleteCommand, Usuario>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioDeleteCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        public async Task<Usuario> Handle(UsuarioDeleteCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(request.Id);
            if (usuario == null)
            {
                throw new ApplicationException($"Error could not be loaded");
            }
            else
            {
                var result = await _usuarioRepository.DeleteAsync(usuario);
                return result;
            }
        }
    }
}
