using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Application.Command.Queries
{
    public class QueryUsuarioParameter
    {
        public string? NomeCompleto { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public UserType? Tipo { get; set; }
        public bool? IsActive { get; set; }

        public int PageNumber { get; set; } = 1; // pagina atual
        public int PageSize { get; set; } = 10; // quant. por pagina

    }
}
