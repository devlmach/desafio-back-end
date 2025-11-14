using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Handler.Usuarios
{
    public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, Usuario>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetUsuarioByIdQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioRepository.GetByIdAsync(request.Id);
        }
    }
}
