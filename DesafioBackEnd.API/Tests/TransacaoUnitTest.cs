using DesafioBackEnd.API.Domain.Entity;
using FluentAssertions;
using DesafioBackEnd.API.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using Xunit;

namespace DesafioBackEnd.API.Tests
{
    public class TransacaoUnitTest
    {
        [Fact(DisplayName = "Create User with valid datas")]
        public void CreateUsuario_WithValidParameters_ResultCreated()
        {
            Action usuario = () => new Usuario
            {
                Id = 1,
                NomeCompleto = "Teste",
                Cpf = "11111111111",
                Email = "email@teste.com",
                Senha = "123",
                Tipo = UserType.LOJISTA,
                Carteira = 15000
            };

            Action usuario2 = () => new Usuario {
                Id = 2,
                NomeCompleto = "Teste",
                Cpf = "11111111111",
                Email = "email@teste.com",
                Senha = "123",
                Tipo = UserType.COMUM,
                Carteira = 15000
            };

            Action result = () => new Transacao
            {
                Id = 1,
                IdSender = 2,
                IdReceiver = 1,
                QuantiaTransferida = 1000
            };

             result.Should().NotThrow<ArgumentException>();
        }

        [Fact(DisplayName = "Create User with invalid idsender null")]
        public void CreateUsuario_WithInvalidNullSender_ResultError()
        {
            // Arrange
            var transacao = new Transacao
            {
                Id = 1,
                IdSender = null, // inválido
                IdReceiver = 1,
                QuantiaTransferida = 1000
            };

            // Act
            Action action = () => Validator.ValidateObject(transacao, new ValidationContext(transacao), true);

            // Assert
            action.Should().Throw<ValidationException>()
                  .WithMessage("O ID do pagador não pode ser vazio");
        }

        [Fact(DisplayName = "Create User with invalid idreceiver null")]
        public void CreateUsuario_WithInvalidNullReceiver_ResultError()
        {
            // Arrange
            var transacao = new Transacao
            {
                Id = 1,
                IdSender = 1, 
                IdReceiver = null,
                QuantiaTransferida = 1000
            };

            // Act
            Action action = () => Validator.ValidateObject(transacao, new ValidationContext(transacao), true);

            // Assert
            action.Should().Throw<ValidationException>()
                  .WithMessage("O ID do recebedor não pode ser vazio");
        }
    }
}
