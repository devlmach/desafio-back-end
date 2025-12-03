using AutoMapper;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using DesafioBackEnd.API.Domain.Entity;
using DesafioBackEnd.API.Application.Dto.Transacoes;
using DesafioBackEnd.API.Application.Command.Transacoes;
using DesafioBackEnd.API.Application.Service;
using DesafioBackEnd.API.Domain.Errors;

namespace DesafioBackEnd.API.Test.Application.Service
{
    public class TransacaoServiceTests
    {
        [Fact(DisplayName = "Create transaction withtout null properties")]
        public async Task CreateTransacao_NoNullValue_ReturnOk()
        {
            var mockMediator = new Mock<IMediator>();
            var mockMapper = new Mock<IMapper>();
            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockTransacaoRepository = new Mock<ITransacaoRepository>();

            var service = new TransacaoService(mockMapper.Object, mockMediator.Object, mockUsuarioRepository.Object, mockHttpContextAccessor.Object, mockTransacaoRepository.Object);

            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                [
                    new Claim("UserId", "1")
                ]))
            };

            mockHttpContextAccessor.Setup(m => m.HttpContext).Returns(httpContext);

            var sender = new Usuario
            {
                Id = 1,
                Carteira = 100,
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                IsActive = true,
            };

            var receiver = new Usuario
            {
                Id = 2,
                Carteira = 100,
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                IsActive = true,
            };

            mockUsuarioRepository.Setup(m => m.GetByIdAsync(sender.Id)).ReturnsAsync(sender);
            mockUsuarioRepository.Setup(m => m.GetByIdAsync(receiver.Id)).ReturnsAsync(receiver);
            mockUsuarioRepository.Setup(m => m.UpdateAsync(It.IsAny<Usuario>())).ReturnsAsync(new Usuario());

            var newTransacao = new CreateTransacaoDto
            {
                IdReceiver = receiver.Id,
                QuantiaTransferida = 50
            };

            var command = new TransacaoCreateCommand
            {
                IdSender = sender.Id,
                IdReceiver = receiver.Id,
                QuantiaTransferida = newTransacao.QuantiaTransferida,
                CreatedAt = DateTime.UtcNow
            };
            mockMapper.Setup(m => m.Map<CreateTransacaoDto, TransacaoCreateCommand>(newTransacao)).Returns(command);

            mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(new Transacao
            {
                IdReceiver = command.IdReceiver,
                QuantiaTransferida = command.QuantiaTransferida,
                CreatedAt = command.CreatedAt
            });

            await service.CreateTransacaoAsync(newTransacao);
            mockUsuarioRepository.Verify(m => m.UpdateAsync(sender), Times.Once());
            mockUsuarioRepository.Verify(m => m.UpdateAsync(receiver), Times.Once());
            Assert.Equal(50, sender.Carteira);
            Assert.Equal(150, receiver.Carteira);
            mockMediator.Verify(m => m.Send(command, default), Times.Once());
        }

        [Fact(DisplayName = "Create transaction with UserType lojista")]
        public async Task CreateTransacao_UserTypeLOJISTA_ReturnBadRequest()
        {
            var mockMediator = new Mock<IMediator>();
            var mockMapper = new Mock<IMapper>();
            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockTransacaoRepository = new Mock<ITransacaoRepository>();

            var service = new TransacaoService(mockMapper.Object, mockMediator.Object, mockUsuarioRepository.Object, mockHttpContextAccessor.Object, mockTransacaoRepository.Object);

            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                [
                    new Claim("UserId", "1")
                ]))
            };

            mockHttpContextAccessor.Setup(m => m.HttpContext).Returns(httpContext);

            var sender = new Usuario
            {
                Id = 1,
                Carteira = 100,
                Tipo = UserType.LOJISTA,
                Role = UserRole.User,
                IsActive = true,
            };

            var receiver = new Usuario
            {
                Id = 2,
                Carteira = 100,
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                IsActive = true,
            };

            mockUsuarioRepository.Setup(m => m.GetByIdAsync(sender.Id)).ReturnsAsync(sender);
            mockUsuarioRepository.Setup(m => m.GetByIdAsync(receiver.Id)).ReturnsAsync(receiver);

            var newTransacao = new CreateTransacaoDto
            {
                IdReceiver = receiver.Id,
                QuantiaTransferida = 50
            };

            var command = new TransacaoCreateCommand
            {
                IdSender = sender.Id,
                IdReceiver = receiver.Id,
                QuantiaTransferida = newTransacao.QuantiaTransferida,
                CreatedAt = DateTime.UtcNow
            };

            mockMapper.Setup(m => m.Map<CreateTransacaoDto, TransacaoCreateCommand>(newTransacao)).Returns(command);
            mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(new Transacao
            {
                IdReceiver = command.IdReceiver,
                QuantiaTransferida = command.QuantiaTransferida,
                CreatedAt = command.CreatedAt
            });

            var ex = await Assert.ThrowsAsync<BadRequestException>(() =>
                service.CreateTransacaoAsync(newTransacao)
            );

            Assert.Equal("LOJISTAS cant make transfer", ex.Message);
        }

        [Fact(DisplayName = "Create transaction with balance lower than the sender wallet")]
        public async Task CreateTransacao_InvalidWallet_ReturnBadRequest()
        {
            var mockMediator = new Mock<IMediator>();
            var mockMapper = new Mock<IMapper>();
            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockTransacaoRepository = new Mock<ITransacaoRepository>();

            var service = new TransacaoService(mockMapper.Object, mockMediator.Object, mockUsuarioRepository.Object, mockHttpContextAccessor.Object, mockTransacaoRepository.Object);

            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                [
                    new Claim("UserId", "1")
                ]))
            };

            mockHttpContextAccessor.Setup(m => m.HttpContext).Returns(httpContext);

            var sender = new Usuario
            {
                Id = 1,
                Carteira = 100,
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                IsActive = true,
            };

            var receiver = new Usuario
            {
                Id = 2,
                Carteira = 100,
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                IsActive = true,
            };

            mockUsuarioRepository.Setup(m => m.GetByIdAsync(sender.Id)).ReturnsAsync(sender);
            mockUsuarioRepository.Setup(m => m.GetByIdAsync(receiver.Id)).ReturnsAsync(receiver);

            var newTransacao = new CreateTransacaoDto
            {
                IdReceiver = receiver.Id,
                QuantiaTransferida = 150
            };

            var command = new TransacaoCreateCommand
            {
                IdSender = sender.Id,
                IdReceiver = receiver.Id,
                QuantiaTransferida = newTransacao.QuantiaTransferida,
                CreatedAt = DateTime.UtcNow
            };

            mockMapper.Setup(m => m.Map<CreateTransacaoDto, TransacaoCreateCommand>(newTransacao)).Returns(command);
            mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(new Transacao
            {
                IdReceiver = command.IdReceiver,
                QuantiaTransferida = command.QuantiaTransferida,
                CreatedAt = command.CreatedAt
            });

            var ex = await Assert.ThrowsAsync<BadRequestException>(() =>
                service.CreateTransacaoAsync(newTransacao)
            );

            Assert.Equal("Sender has less then the quantity to complete the transfer", ex.Message);
        }

        [Fact(DisplayName = "Create transaction with sender IsActive = false")]
        public async Task CreateTransacao_InvalidFalseIsActiveSender_ReturnBadRequest()
        {
            var mockMediator = new Mock<IMediator>();
            var mockMapper = new Mock<IMapper>();
            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockTransacaoRepository = new Mock<ITransacaoRepository>();

            var service = new TransacaoService(mockMapper.Object, mockMediator.Object, mockUsuarioRepository.Object, mockHttpContextAccessor.Object, mockTransacaoRepository.Object);

            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                [
                    new Claim("UserId", "1")
                ]))
            };

            mockHttpContextAccessor.Setup(m => m.HttpContext).Returns(httpContext);

            var sender = new Usuario
            {
                Id = 1,
                Carteira = 100,
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                IsActive = false,
            };

            var receiver = new Usuario
            {
                Id = 2,
                Carteira = 100,
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                IsActive = true,
            };

            mockUsuarioRepository.Setup(m => m.GetByIdAsync(sender.Id)).ReturnsAsync(sender);
            mockUsuarioRepository.Setup(m => m.GetByIdAsync(receiver.Id)).ReturnsAsync(receiver);

            var newTransacao = new CreateTransacaoDto
            {
                IdReceiver = receiver.Id,
                QuantiaTransferida = 150
            };

            var command = new TransacaoCreateCommand
            {
                IdSender = sender.Id,
                IdReceiver = receiver.Id,
                QuantiaTransferida = newTransacao.QuantiaTransferida,
                CreatedAt = DateTime.UtcNow
            };

            mockMapper.Setup(m => m.Map<CreateTransacaoDto, TransacaoCreateCommand>(newTransacao)).Returns(command);
            mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(new Transacao
            {
                IdReceiver = command.IdReceiver,
                QuantiaTransferida = command.QuantiaTransferida,
                CreatedAt = command.CreatedAt
            });

            var ex = await Assert.ThrowsAsync<BadRequestException>(() =>
                service.CreateTransacaoAsync(newTransacao)
            );

            Assert.Equal("Sender user cannot be found.", ex.Message);
        }

        [Fact(DisplayName = "Create transaction with receiver IsActive = false")]
        public async Task CreateTransacao_InvalidFalseIsActiveReceiver_ReturnBadRequest()
        {
            var mockMediator = new Mock<IMediator>();
            var mockMapper = new Mock<IMapper>();
            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockTransacaoRepository = new Mock<ITransacaoRepository>();

            var service = new TransacaoService(mockMapper.Object, mockMediator.Object, mockUsuarioRepository.Object, mockHttpContextAccessor.Object, mockTransacaoRepository.Object);

            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                [
                    new Claim("UserId", "1")
                ]))
            };

            mockHttpContextAccessor.Setup(m => m.HttpContext).Returns(httpContext);

            var sender = new Usuario
            {
                Id = 1,
                Carteira = 100,
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                IsActive = true,
            };

            var receiver = new Usuario
            {
                Id = 2,
                Carteira = 100,
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                IsActive = false,
            };

            mockUsuarioRepository.Setup(m => m.GetByIdAsync(sender.Id)).ReturnsAsync(sender);
            mockUsuarioRepository.Setup(m => m.GetByIdAsync(receiver.Id)).ReturnsAsync(receiver);

            var newTransacao = new CreateTransacaoDto
            {
                IdReceiver = receiver.Id,
                QuantiaTransferida = 150
            };

            var command = new TransacaoCreateCommand
            {
                IdSender = sender.Id,
                IdReceiver = receiver.Id,
                QuantiaTransferida = newTransacao.QuantiaTransferida,
                CreatedAt = DateTime.UtcNow
            };

            mockMapper.Setup(m => m.Map<CreateTransacaoDto, TransacaoCreateCommand>(newTransacao)).Returns(command);
            mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(new Transacao
            {
                IdReceiver = command.IdReceiver,
                QuantiaTransferida = command.QuantiaTransferida,
                CreatedAt = command.CreatedAt
            });

            var ex = await Assert.ThrowsAsync<BadRequestException>(() =>
                service.CreateTransacaoAsync(newTransacao)
            );

            Assert.Equal("Receiver user cannot be found.", ex.Message);
        }

        [Fact(DisplayName = "Create transaction with balance lower than 0")]
        public async Task CreateTransacao_InvalidBalance_ReturnBadRequest()
        {
            var mockMediator = new Mock<IMediator>();
            var mockMapper = new Mock<IMapper>();
            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockTransacaoRepository = new Mock<ITransacaoRepository>();

            var service = new TransacaoService(mockMapper.Object, mockMediator.Object, mockUsuarioRepository.Object, mockHttpContextAccessor.Object, mockTransacaoRepository.Object);

            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(
                [
                    new Claim("UserId", "1")
                ]))
            };

            mockHttpContextAccessor.Setup(m => m.HttpContext).Returns(httpContext);

            var sender = new Usuario
            {
                Id = 1,
                Carteira = 100,
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                IsActive = true,
            };

            var receiver = new Usuario
            {
                Id = 2,
                Carteira = 100,
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                IsActive = true,
            };

            mockUsuarioRepository.Setup(m => m.GetByIdAsync(sender.Id)).ReturnsAsync(sender);
            mockUsuarioRepository.Setup(m => m.GetByIdAsync(receiver.Id)).ReturnsAsync(receiver);

            var newTransacao = new CreateTransacaoDto
            {
                IdReceiver = receiver.Id,
                QuantiaTransferida = -1
            };

            var command = new TransacaoCreateCommand
            {
                IdSender = sender.Id,
                IdReceiver = receiver.Id,
                QuantiaTransferida = newTransacao.QuantiaTransferida,
                CreatedAt = DateTime.UtcNow
            };

            mockMapper.Setup(m => m.Map<CreateTransacaoDto, TransacaoCreateCommand>(newTransacao)).Returns(command);
            mockMediator.Setup(m => m.Send(command, default)).ReturnsAsync(new Transacao
            {
                IdReceiver = command.IdReceiver,
                QuantiaTransferida = command.QuantiaTransferida,
                CreatedAt = command.CreatedAt
            });

            var ex = await Assert.ThrowsAsync<BadRequestException>(() =>
                service.CreateTransacaoAsync(newTransacao)
            );

            Assert.Equal("The money you wanna send cannot be lower then 0", ex.Message);
        }
    }
}
