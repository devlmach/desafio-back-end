using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Application.Command.Queries
{
    public class QueryParameter
    {
        public string? NomeCompleto { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public UserType? Tipo { get; set; }
        public bool? IsActive { get; set; }
    }
}
