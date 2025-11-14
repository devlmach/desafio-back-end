using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Domain.Entity
{
    public class Usuario : ClassBase
    {
        [Required(ErrorMessage = "O Campo nome não pode ser vazio")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo CPF não pode ser vazio")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 números")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Email não pode ser nulo")]
        public string Email { get; set; }

        public string Senha { get; set; }

        [Required(ErrorMessage = "O tipo do usuário não pode ser nulo")]
        public UserType? Tipo { get; set; }

        [Required]
        public decimal Carteira { get; set; }

        public Usuario() {}

        public Usuario(long id, string nomeCompleto, string cpf, string email, string senha, UserType tipo, decimal carteira, DateTime createdAt, DateTime updatedAt, bool isActive)
        {
            Id = id;
            NomeCompleto = nomeCompleto;
            Cpf = cpf;
            Email = email;
            Senha = senha;
            Tipo = tipo;
            Carteira = carteira;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsActive = isActive;
        }

        public Usuario(string nomeCompleto, string cpf, string email, string senha, UserType tipo, decimal carteira, DateTime createdAt, DateTime updatedAt, bool isActive)
        {
            NomeCompleto = nomeCompleto;
            Cpf = cpf;
            Email = email;
            Senha = senha;
            Tipo = tipo;
            Carteira = carteira;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsActive = isActive;
        }

        public void Update(string nome, string cpf, string email, string senha,
            UserType tipo, decimal carteira, DateTime createdAt, DateTime updatedAt,
            bool isActive)
        {
            NomeCompleto = nome;
            Cpf = cpf;
            Email = email;
            Senha = senha;
            Tipo = tipo;
            Carteira = carteira;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsActive = isActive;
        }
    }
}
