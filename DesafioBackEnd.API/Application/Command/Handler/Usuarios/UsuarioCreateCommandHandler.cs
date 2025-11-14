using DesafioBackEnd.API.Application.Command.Usuarios;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Handler.Usuarios
{
    public class UsuarioCreateCommandHandler : IRequestHandler<UsuarioCreateCommand, Usuario>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioCreateCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        public async Task<Usuario> Handle(UsuarioCreateCommand request, CancellationToken cancellationToken)
        {
            var usuario = new Usuario(request.NomeCompleto, request.Cpf, request.Email, request.Senha, request.Tipo, request.Carteira, request.CreatedAt,
                request.UpdatedAt, request.IsActive);
            if (usuario == null)
            {
                throw new ApplicationException("error creating entity");

            }
            else
            {
                return await _usuarioRepository.AddAsync(usuario);
            }
        }
    }
}
