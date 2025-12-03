using DesafioBackEnd.API.Application.Dto.Transacoes;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Test.Application.Dto.Transacao
{
    public class CreateTransacaoDtoTests
    {
        [Fact(DisplayName = "Create transacao com valid properties")]
        public void CreateTransacao_WithValidParameters_ReturnCreated()
        {
            Action action = () => new CreateTransacaoDto
            {
                IdReceiver = 2,
                QuantiaTransferida = 50
            };

            action.Should().NotThrow<InvalidOperationException>();
        }

        [Fact(DisplayName = "Create transacao com idSender null")]
        public void CreateTransacao_WithIdSenderNull_ResultErrorMessage()
        {
            var transfer = new CreateTransacaoDto
            {
                IdReceiver = null!,
                QuantiaTransferida = 50
            };

            Action action = () => Validator.ValidateObject(transfer, new ValidationContext(transfer), validateAllProperties: true);
            action.Should().Throw<ValidationException>().WithMessage("O ID do recebedor não pode ser vazio");
        }

        [Fact(DisplayName = "Create transacao com QuantiaTransferida null")]
        public void CreateTransacao_WithQuantiaTransferidaNull_ResultErrorMessage()
        {
            var transacao = new CreateTransacaoDto
            {
                IdReceiver = 2,
                QuantiaTransferida = null
            };

            Action action = () => Validator.ValidateObject(transacao, new ValidationContext(transacao), validateAllProperties: true);
            action.Should().Throw<ValidationException>().WithMessage("O valor a ser transferido não pode ser vazio");
        }
    }
}
