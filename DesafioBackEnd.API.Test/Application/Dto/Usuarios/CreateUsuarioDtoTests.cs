using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Domain.Entity;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Test.Application.Dto.Usuarios
{
    public class CreateUsuarioDtoTests
    {
        [Fact(DisplayName = "Create user with valid properties")]
        public void CreateUsuario_WithValidParameters_ReturnCreated()
        {
            Action action = () => new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = "48405315870",
                Email = "teste@email.com",
                Senha = "@Jaqrafa@2206@#%",
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            action.Should().NotThrow<InvalidOperationException>();
        }

        [Fact(DisplayName = "Create user with null name")]
        public void CreateUsuario_WithNullName_ResultErrorMessage()
        {
            var user = new CreateUsuarioDto
            {
                NomeCompleto = null!,
                Cpf = "48405315870",
                Email = "teste@email.com",
                Senha = "@Jaqrafa@2206@#%",
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            Action action = () => Validator.ValidateObject(user, new ValidationContext(user), validateAllProperties: true);
            action.Should().Throw<ValidationException>().WithMessage($"O Campo NomeCompleto não pode ser vazio");
        }

        [Fact(DisplayName = "Create user with null cpf")]
        public void CreateUsuario_WithNullCpf_ResultErrorMessage()
        {
            var user = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = null!,
                Email = "teste@email.com",
                Senha = "@Jaqrafa@2206@#%",
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            Action action = () => Validator.ValidateObject(user, new ValidationContext(user), validateAllProperties: true);
            action.Should().Throw<ValidationException>().WithMessage($"O Campo Cpf não pode ser vazio");
        }

        [Fact(DisplayName = "Create user with less than 11 numbers")]
        public void CreateUsuario_WithLessThan11_ResultErrorMessage()
        {
            var user = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael Luna",
                Cpf = "4840531587", // 10 caracteres
                Email = "teste@email.com",
                Senha = "@Jaqrafa@2206@#%",
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            Action action = () => Validator.ValidateObject(user, new ValidationContext(user), validateAllProperties: true);
            action.Should().Throw<ValidationException>().WithMessage($"O Cpf deve ter 11 números");
        }

        [Fact(DisplayName = "Create user with more than 11 numbers")]
        public void CreateUsuario_WithMoreThan11_ResultErrorMessage()
        {
            var user = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael Luna",
                Cpf = "484053158701", // 12 caracteres
                Email = "teste@email.com",
                Senha = "@Jaqrafa@2206@#%",
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            Action action = () => Validator.ValidateObject(user, new ValidationContext(user), validateAllProperties: true);
            action.Should().Throw<ValidationException>().WithMessage($"O Cpf deve ter 11 números");
        }

        [Fact(DisplayName = "Create user with null email")]
        public void CreateUsuario_WithNullEmail_ResultErrorMessage()
        {
            var user = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = "48405315870",
                Email = null!,
                Senha = "@Jaqrafa@2206@#%",
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            Action action = () => Validator.ValidateObject(user, new ValidationContext(user), validateAllProperties: true);
            action.Should().Throw<ValidationException>().WithMessage($"O Campo Email não pode ser nulo");
        }

        [Fact(DisplayName = "Create user with null senha")]
        public void CreateUsuario_WithNullSenha_ResultErrorMessage()
        {
            var user = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = "48405315870",
                Email = "teste@email.com",
                Senha = null!,
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            Action action = () => Validator.ValidateObject(user, new ValidationContext(user), validateAllProperties: true);
            action.Should().Throw<ValidationException>().WithMessage($"Insira uma senha");
        }

        [Fact(DisplayName = "Create user with null tipo")]
        public void CreateUsuario_WithNullTipo_ReturnErrorMessage()
        {
            var user = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = "48405315870",
                Email = "teste@email.com",
                Senha = "@Jaqrafa@2206@#%",
                Tipo = null!,
                Role = UserRole.Admin
            };

            Action action = () => Validator.ValidateObject(user, new ValidationContext(user), validateAllProperties: true);
            action.Should().Throw<ValidationException>().WithMessage("O tipo do usuário não pode ser nulo");
        }

        [Fact(DisplayName = "Create user with null role")]
        public void CreateUsuario_WithNullRole_ReturnErrorMessage()
        {
            var user = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = "48405315870",
                Email = "teste@email.com",
                Senha = "@Jaqrafa@2206@#%",
                Tipo = UserType.COMUM,
                Role = null
            };

            Action action = () => Validator.ValidateObject(user, new ValidationContext(user), validateAllProperties: true);
            action.Should().Throw<ValidationException>().WithMessage("Insira uma função para o usuário");
        }
    }
}
