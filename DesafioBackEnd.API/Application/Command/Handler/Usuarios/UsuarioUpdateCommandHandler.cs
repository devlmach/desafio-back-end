using DesafioBackEnd.API.Application.Command.Usuarios;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Handler.Usuarios
{
    public class UsuarioUpdateCommandHandler : IRequestHandler<UsuarioUpdateCommand, Usuario>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioUpdateCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Handle(UsuarioUpdateCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(request.Id);
            if (usuario == null)
            {
                throw new ApplicationException($"Error could not be found");
            }
            else
            {
                usuario.Update(request.NomeCompleto, request.Cpf, request.Email, request.Senha, request.Tipo,
                    request.Carteira, request.CreatedAt, request.UpdatedAt, request.IsActive);
                return await _usuarioRepository.UpdateAsync(usuario);
            }
        }
    }
}
