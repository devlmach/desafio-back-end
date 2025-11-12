using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Entity
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Campo nome não pode ser vazio")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo CPF não pode ser vazio")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 números")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Email não pode ser nulo")]
        public string Email { get; set; }

        public string Senha { get; set; }

        [Required(ErrorMessage = "O tipo do usuário não pode ser nulo")]
        //[AllowedValues(UserType.COMUM, UserType.LOJISTA, ErrorMessage = "Valor inválido, cadastre como COMUM ou LOJISTA")]
        public UserType? Tipo { get; set; }

        [Required]
        public decimal Carteira { get; set; }

        public Usuario() {}

        public Usuario(int id, string nomeCompleto, string cpf, string email, string senha, UserType tipo, decimal carteira)
        {
            Id = id;
            NomeCompleto = nomeCompleto;
            Cpf = cpf;
            Email = email;
            Senha = senha;
            Tipo = tipo;
            Carteira = carteira;
        }
    }
}
