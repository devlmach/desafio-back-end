using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Handler.Usuarios
{
    public class GetUsuariosQueryHandler : IRequestHandler<GetUsuariosQuery, IEnumerable<Usuario>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public GetUsuariosQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        public async Task<IEnumerable<Usuario>> Handle(GetUsuariosQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioRepository.GetUsuariosAsync();
        }
    }
}
