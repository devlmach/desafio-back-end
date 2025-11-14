using DesafioBackEnd.API.Domain.Entity;
using DesafioBackEnd.API.Entity;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using Xunit;

namespace DesafioBackEnd.API.Tests
{
    public class UsuarioUnitTest
    {
        // exemplo nome metodo CreateCategory_WithValidParameters_ResultObjectValidState
        [Fact(DisplayName = "Create User with valid datas")]
        public void CreateUsuario_WithValidParameters_ResultCreated()
        {
            // Arranje
            Action action = () => new Usuario
            {
                Id = 1,
                NomeCompleto = "Teste",
                Cpf = "11111111111",
                Email = "email@teste.com",
                Senha = "123",
                Tipo = UserType.LOJISTA,
                Carteira = 15000
            };
            // Act
            action.Should().NotThrow<ArgumentException>();
            // Assert
        }

        [Fact(DisplayName = "Create User With null name")]
        public void CreateUser_WithInvalidName_ResultError()
        {
            // Arranje - cria usuario novo
            var usuario = new Usuario
            {
                Id = 1,
                NomeCompleto = "",
                Cpf = "11111111111",
                Email = "email@teste.com",
                Senha = "123",
                Tipo = UserType.LOJISTA,
                Carteira = 15000
            };

            // Act - açao a ser feita com o usuario novo
            Action action = () => Validator.ValidateObject(usuario, new ValidationContext(usuario), true);

            // retorno da ação
            action.Should().Throw<ValidationException>().WithMessage("O Campo nome não pode ser vazio");
        }

        [Fact(DisplayName = "Create User With null cpf")]
        public void CreateUser_WithInvalidCpfNull_ResultError()
        {
            // Arranje - cria usuario novo
            var usuario = new Usuario
            {
                Id = 1,
                NomeCompleto = "Teste",
                Cpf = "",
                Email = "email@teste.com",
                Senha = "123",
                Tipo = UserType.LOJISTA,
                Carteira = 15000
            };

            // Act - açao a ser feita com o usuario novo
            Action action = () => Validator.ValidateObject(usuario, new ValidationContext(usuario), true);

            // retorno da ação
            action.Should().Throw<ValidationException>().WithMessage("O campo CPF não pode ser vazio");
        }

        [Fact(DisplayName = "Create User With cpf longer than 11 characteres")]
        public void CreateUser_WithInvalidLongerCpf_ResultError()
        {
            // Arranje - cria usuario novo
            var usuario = new Usuario
            {
                Id = 1,
                NomeCompleto = "Teste",
                Cpf = "111111111111", //12
                Email = "email@teste.com",
                Senha = "123",
                Tipo = UserType.LOJISTA,
                Carteira = 15000
            };

            // Act - açao a ser feita com o usuario novo
            Action action = () => Validator.ValidateObject(usuario, new ValidationContext(usuario), true);

            // retorno da ação
            action.Should().Throw<ValidationException>().WithMessage("O CPF deve ter 11 números");
        }

        [Fact(DisplayName = "Create User With cpf shorter than 11 characteres")]
        public void CreateUser_WithInvalidshorterCpf_ResultError()
        {
            // Arranje - cria usuario novo
            var usuario = new Usuario
            {
                Id = 1,
                NomeCompleto = "Teste",
                Cpf = "1111111111", //10
                Email = "email@teste.com",
                Senha = "123",
                Tipo = UserType.LOJISTA,
                Carteira = 15000
            };

            // Act - açao a ser feita com o usuario novo
            Action action = () => Validator.ValidateObject(usuario, new ValidationContext(usuario), true);

            // retorno da ação
            action.Should().Throw<ValidationException>().WithMessage("O CPF deve ter 11 números");
        }

        [Fact(DisplayName = "Create User With null email")]
        public void CreateUser_WithInvalidEmailNull_ResultError()
        {
            // Arranje - cria usuario novo
            var usuario = new Usuario
            {
                Id = 1,
                NomeCompleto = "Teste",
                Cpf = "11111111111",
                Email = "",
                Senha = "123",
                Tipo = UserType.LOJISTA,
                Carteira = 15000
            };

            // Act - açao a ser feita com o usuario novo
            Action action = () => Validator.ValidateObject(usuario, new ValidationContext(usuario), true);

            // retorno da ação
            action.Should().Throw<ValidationException>().WithMessage("O campo Email não pode ser nulo");
        }

        [Fact(DisplayName = "Create User with null password")]
        public void CreateUsuario_WithValidParameter_ResultCreated()
        {
            // Arranje
            Action action = () => new Usuario
            {
                Id = 1,
                NomeCompleto = "Teste",
                Cpf = "11111111111",
                Email = "email@teste.com",
                Senha = "",
                Tipo = UserType.LOJISTA,
                Carteira = 15000
            };
            // Act
            action.Should().NotThrow<ArgumentException>();
            // Assert
        }

        [Fact(DisplayName = "Create User With null usertype")]
        public void CreateUser_WithInvalidUserType_ResultError()
        {
            // Arranje - cria usuario novo
            var usuario = new Usuario
            {
                Id = 1,
                NomeCompleto = "Teste",
                Cpf = "11111111111",
                Email = "email@teste.com",
                Senha = "123",
                Tipo = null,
                Carteira = 15000
            };

            // Act - açao a ser feita com o usuario novo
            Action action = () => Validator.ValidateObject(usuario, new ValidationContext(usuario), true);

            // retorno da ação
            action.Should().Throw<ValidationException>().WithMessage("O tipo do usuário não pode ser nulo");
        }

    }
}
