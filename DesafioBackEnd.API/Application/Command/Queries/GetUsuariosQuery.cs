using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;

namespace DesafioBackEnd.API.Application.Command.Queries
{
    public class GetUsuariosQuery : IRequest<IEnumerable<DetailUsuarioDto>>
    {
        public string? NomeCompleto { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public UserType? Tipo { get; set; }
        public UserRole? Role { get; set; }
        public bool? IsActive { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
