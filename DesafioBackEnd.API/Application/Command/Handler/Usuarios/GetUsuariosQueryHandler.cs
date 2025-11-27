using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Handler.Usuarios
{
    public class GetUsuariosQueryHandler : IRequestHandler<GetUsuariosQuery, IEnumerable<DetailUsuarioDto>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public GetUsuariosQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        public async Task<IEnumerable<DetailUsuarioDto>> Handle(GetUsuariosQuery request, CancellationToken cancellationToken)
        {
            return await _usuarioRepository.GetUsuariosAsync(request.NomeCompleto, request.Cpf, request.Email, request.Tipo, request.Role, request.IsActive, request.PageNumber, request.PageSize);
        }
    }
}
