using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Dto.Transacoes;
using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Controllers;
using DesafioBackEnd.API.Domain.Entity;
using DesafioBackEnd.API.Domain.Errors;
using DesafioBackEnd.API.Integrations.Authorization.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace DesafioBackEnd.API.Test.Controller
{
    public class TransacaoControllerTests
    {

        [Fact(DisplayName = "POST /api/transacao/transfer - retorna 200 OK com transação criada")]
        public async Task CreateTransaction_ReturnsOkWithCreatedDto()
        {
            long senderId = 1;
            // Arrange
            var request = new CreateTransacaoDto
            {
                IdReceiver = 2,
                QuantiaTransferida = 50
            };

            var created = new DetailTransacaoDto
            {
                Id = 123,
                IdSender = senderId,
                IdReceiver = request.IdReceiver,
                QuantiaTransferida = request.QuantiaTransferida,
                CreatedAt = DateTime.UtcNow
            };

            var mockAuthentication = new Mock<IAuthorizationsService>(); // ajuste seu tipo real
            mockAuthentication.Setup(m => m.IsAuthorizedAsync()).ReturnsAsync(true);

            var mockTransacaoService = new Mock<ITransacaoService>();
            mockTransacaoService.Setup(s => s.CreateTransacaoAsync(It.IsAny<CreateTransacaoDto>())).Callback<CreateTransacaoDto>(d =>
            {
                d.IdReceiver = created.IdReceiver;
                d.QuantiaTransferida = created.QuantiaTransferida;
            }).Returns(Task.CompletedTask);

            var mockTransacaoController = new TransacaoController(mockTransacaoService.Object, mockAuthentication.Object);

            // Usuário autenticado
            mockTransacaoController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, "10"),
                        new Claim(ClaimTypes.Role, UserRole.User.ToString())
                    }, "mock"))
                }
            };
            
            // Act
            var result = await mockTransacaoController.CreateTransaction(request);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

            var body = Assert.IsType<CreateTransacaoDto>(ok.Value);

            mockTransacaoService.Verify(s => s.CreateTransacaoAsync(It.Is<CreateTransacaoDto>(d => d == request)), Times.Once);
        }

        [Fact(DisplayName = "GET /api/transacao/id - retorna 403 FORBIDDEN - user busca id de transação sem id dele")]
        public async Task GetById_UserAccessingOtherUserTransaction_ThrowsBadRequest()
        {
            // Arrange
            long loggedUserId = 1;
            long transacaoId = 10;

            var mockTransacaoService = new Mock<ITransacaoService>();
            var mockAuthorization = new Mock<IAuthorizationsService>();
            var fakeTransacao = new DetailTransacaoDto
            {
                Id = transacaoId,
                IdSender = 2,     // outro usuário
                IdReceiver = 3, // outro usuário
                QuantiaTransferida = 50,
                CreatedAt = DateTime.UtcNow
            };

            mockTransacaoService.Setup(s => s.GetTransacaoByIdAsync(transacaoId))
                       .ReturnsAsync(fakeTransacao);

            var mockTransacaoController = new TransacaoController(mockTransacaoService.Object, mockAuthorization.Object);

            // Criar identidade com role User + claim UserId
            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim("UserId", loggedUserId.ToString()),
                new Claim(ClaimTypes.Role, "User")
            ], "mock"));

            mockTransacaoController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act & Assert
            await Assert.ThrowsAsync<ForbiddenException>(async () =>
                await mockTransacaoController.GetById(transacaoId)
            );
        }

        [Fact(DisplayName = "GET /api/transacao/id - retorna 200 OK - user busca id de transação com id do user logado sendo IdSender")]
        public async Task GetById_UserIsSender_ReturnsOkWithDto()
        {
            // Arrange
            long loggedUserId = 1;
            long transacaoId = 10;

            var expectedDto = new DetailTransacaoDto
            {
                Id = transacaoId,
                IdSender = loggedUserId,
                IdReceiver = 99
            };

            var mockService = new Mock<ITransacaoService>();
            var mockAuthorization = new Mock<IAuthorizationsService>();
            mockService
                .Setup(s => s.GetTransacaoByIdAsync(transacaoId))
                .ReturnsAsync(expectedDto);

            var mockTransacaoController = new TransacaoController(mockService.Object, mockAuthorization.Object);

            // Simula Claims do usuário
            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim("UserId", loggedUserId.ToString()),
                new Claim(ClaimTypes.Role, "User")
            ]));

            mockTransacaoController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await mockTransacaoController.GetById(transacaoId);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<DetailTransacaoDto>(ok.Value);

            Assert.Equal(expectedDto.Id, dto.Id);
            Assert.Equal(expectedDto.IdSender, dto.IdSender);
            Assert.Equal(expectedDto.IdReceiver, dto.IdReceiver);
        }

        [Fact(DisplayName = "GET /api/transacao/id - retorna 200 OK - user busca id de transação com id do user logado sendo IdReceiver")]
        public async Task GetById_UserIsReceiver_ReturnsOkWithDto()
        {
            // Arrange
            long loggedUserId = 1;
            long transacaoId = 10;

            var expectedDto = new DetailTransacaoDto
            {
                Id = transacaoId,
                IdSender = 99,
                IdReceiver = loggedUserId
            };

            var mockTransacaoService = new Mock<ITransacaoService>();
            var mockAuthorization = new Mock<IAuthorizationsService>();
            mockTransacaoService
                .Setup(s => s.GetTransacaoByIdAsync(transacaoId))
                .ReturnsAsync(expectedDto);

            var mockTransacaoController = new TransacaoController(mockTransacaoService.Object, mockAuthorization.Object);

            // Simula Claims
            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim("UserId", loggedUserId.ToString()),
                new Claim(ClaimTypes.Role, "User")
            ]));

            mockTransacaoController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await mockTransacaoController.GetById(transacaoId);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<DetailTransacaoDto>(ok.Value);

            Assert.Equal(expectedDto.Id, dto.Id);
            Assert.Equal(expectedDto.IdSender, dto.IdSender);
            Assert.Equal(expectedDto.IdReceiver, dto.IdReceiver);
        }

        [Fact(DisplayName = "GET  /api/transacao - retorna 200 e lista de DetailTransacaoDto onde id do user logado é IdSender ou IdReceiver")]
        public async Task GetAllTransacoes_UserRole_ReturnsUserTransactions()
        {
            // Arrange
            long loggedUserId = 5;

            var query = new QueryTransacaoParameter
            {
                PageNumber = 1,
                PageSize = 10
            };

            var expectedList = new List<DetailTransacaoDto>
            {
                new() { Id = 1, IdSender = loggedUserId, IdReceiver = 10, QuantiaTransferida = 50, CreatedAt = DateTime.UtcNow },
                new() { Id = 2, IdSender = 11, IdReceiver = loggedUserId, QuantiaTransferida = 50, CreatedAt = DateTime.UtcNow },
                new() { Id = 3, IdSender = loggedUserId, IdReceiver = 6, QuantiaTransferida = 50, CreatedAt = DateTime.UtcNow }
            };

            var mockTransacaoService = new Mock<ITransacaoService>();
            var mockAuthorization = new Mock<IAuthorizationsService>();
            mockTransacaoService
                .Setup(s => s.GetTransacoesUserAsync(loggedUserId, query.PageNumber, query.PageSize))
                .ReturnsAsync(expectedList);

            var mockTransacaoController = new TransacaoController(mockTransacaoService.Object, mockAuthorization.Object);

            // Simula Claims do usuário autenticado
            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim("UserId", loggedUserId.ToString()),
                new Claim(ClaimTypes.Role, "User")
            ], "mock"));

            mockTransacaoController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await mockTransacaoController.GetAllTransacoes(query);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsAssignableFrom<IEnumerable<DetailTransacaoDto>>(okResult.Value);

            Assert.Equal(expectedList.Count, list.Count());
            Assert.Equal(expectedList.First().Id, list.First().Id);

            // Confirma que chamou o método correto
            mockTransacaoService.Verify(s => s.GetTransacoesUserAsync(loggedUserId, query.PageNumber, query.PageSize), Times.Once);

            // Confirma que NÃO chamou o método de Admin
            mockTransacaoService.Verify(s => s.GetTransacoesAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Fact(DisplayName = "GET  /api/transacao - retorna 200 e lista de DetailTransacaoDto onde o user logado é Admin")]
        public async Task GetAllTransacoes_AdminRole_ReturnsAllTransactions()
        {
            // Arrange
            var query = new QueryTransacaoParameter
            {
                PageNumber = 1,
                PageSize = 10
            };

            var expectedList = new List<DetailTransacaoDto>
            {
                new() { Id = 100, IdSender = 1, IdReceiver = 2, QuantiaTransferida = 50, CreatedAt = DateTime.UtcNow },
                new() { Id = 101, IdSender = 3, IdReceiver = 4, QuantiaTransferida = 50, CreatedAt = DateTime.UtcNow },
                new() { Id = 102, IdSender = 2, IdReceiver = 3, QuantiaTransferida = 50, CreatedAt = DateTime.UtcNow }
            };

            var mockTransacaoService = new Mock<ITransacaoService>();
            var mockAuthorization = new Mock<IAuthorizationsService>();

            mockTransacaoService
                .Setup(s => s.GetTransacoesAsync(query.PageNumber, query.PageSize))
                .ReturnsAsync(expectedList);

            var mockTransacaoController = new TransacaoController(mockTransacaoService.Object, mockAuthorization.Object);

            // Simula Claims do usuário com role Admin
            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim("UserId", "999"), // qualquer valor
                new Claim(ClaimTypes.Role, UserRole.Admin.ToString())
            ], "mock"));

            mockTransacaoController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await mockTransacaoController.GetAllTransacoes(query);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var list = Assert.IsType<IEnumerable<DetailTransacaoDto>>(okResult.Value, exactMatch: false);

            Assert.Equal(expectedList.Count, list.Count());
            Assert.Equal(expectedList.First().Id, list.First().Id);

            // Verifica se chamou o método correto (Admin)
            mockTransacaoService.Verify(s => s.GetTransacoesAsync(query.PageNumber, query.PageSize), Times.Once);

            // Verifica se NÃO chamou o método de User
            mockTransacaoService.Verify(s => s.GetTransacoesUserAsync(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
    }
}
