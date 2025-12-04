using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Controllers;
using DesafioBackEnd.API.Domain.Entity;
using DesafioBackEnd.API.Domain.Errors;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace DesafioBackEnd.API.Test.Controller
{
    public class UsuarioControllerTests
    {
        [Fact(DisplayName = "POST /api/usuario - deve retornar 200 Ok")]
        public async Task CreateUsuario_ValidParameters_Return200Ok()
        {
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var usuario = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = "484-5315870",
                Email = "email@teste.com",
                Senha = "Saysay@2025!@",
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            var result = await mockUsuarioController.CreateUsuario(usuario);

            var actionResult = Assert.IsType<ActionResult<CreateUsuarioDto>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            Assert.Equal(200, okResult.StatusCode); // retorna 200 OK
        }

        [Fact(DisplayName = "POST /api/usuario - deve retornar 400 BadRequest")]
        public async Task CreateUsuario_InvalidNameNull_Return400BadRequest()
        {
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var usuario = new CreateUsuarioDto
            {
                NomeCompleto = null!,
                Cpf = "484-5315870",
                Email = "email@teste.com",
                Senha = "Saysay@2025!@",
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            mockUsuarioController.ModelState.AddModelError("NomeCompleto", "Invalid data.");

            var result = await mockUsuarioController.CreateUsuario(usuario);

            var actionResult = Assert.IsType<ActionResult<CreateUsuarioDto>>(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.Equal(400, badRequest.StatusCode); // retorna 400 bad request
        }

        [Fact(DisplayName = "POST /api/usuario - deve retornar 400 BadRequest")]
        public async Task CreateUsuario_InvalidCpfNull_Return400BadRequest()
        {
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var usuario = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = null!,
                Email = "email@teste.com",
                Senha = "Saysay@2025!@",
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            mockUsuarioController.ModelState.AddModelError("Cpf", "Invalid data.");

            var result = await mockUsuarioController.CreateUsuario(usuario);

            var actionResult = Assert.IsType<ActionResult<CreateUsuarioDto>>(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.Equal(400, badRequest.StatusCode); // retorna 400 bad request
        }

        [Fact(DisplayName = "POST /api/usuario - deve retornar 400 BadRequest")]
        public async Task CreateUsuario_InvalidEmailNull_Return400BadRequest()
        {
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var usuario = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = "48405315870",
                Email = null!,
                Senha = "Saysay@2025!@",
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            mockUsuarioController.ModelState.AddModelError("Cpf", "Invalid data.");

            var result = await mockUsuarioController.CreateUsuario(usuario);

            var actionResult = Assert.IsType<ActionResult<CreateUsuarioDto>>(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.Equal(400, badRequest.StatusCode); // retorna 400 bad request
        }

        [Fact(DisplayName = "POST /api/usuario - deve retornar 400 BadRequest")]
        public async Task CreateUsuario_InvalidSenhaNull_Return400BadRequest()
        {
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var usuario = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = "48405315870",
                Email = "email@teste.com",
                Senha = null!,
                Tipo = UserType.COMUM,
                Role = UserRole.Admin
            };

            mockUsuarioController.ModelState.AddModelError("Cpf", "Invalid data.");

            var result = await mockUsuarioController.CreateUsuario(usuario);

            var actionResult = Assert.IsType<ActionResult<CreateUsuarioDto>>(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.Equal(400, badRequest.StatusCode); // retorna 400 bad request
        }

        [Fact(DisplayName = "POST /api/usuario - deve retornar 400 BadRequest")]
        public async Task CreateUsuario_InvalidTipoNull_Return400BadRequest()
        {
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var usuario = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = "48405315870",
                Email = "email@teste.com",
                Senha = "Saysay@2025!@",
                Tipo = null!,
                Role = UserRole.Admin
            };

            mockUsuarioController.ModelState.AddModelError("Cpf", "Invalid data.");

            var result = await mockUsuarioController.CreateUsuario(usuario);

            var actionResult = Assert.IsType<ActionResult<CreateUsuarioDto>>(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.Equal(400, badRequest.StatusCode); // retorna 400 bad request
        }

        [Fact(DisplayName = "POST /api/usuario/id - deve retornar 400 BadRequest")]
        public async Task CreateUsuario_InvalidRoleNull_Return400BadRequest()
        {
            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var usuario = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael",
                Cpf = "48405315870",
                Email = "email@teste.com",
                Senha = "Saysay@2025!@",
                Tipo = UserType.COMUM,
                Role = null!
            };

            mockUsuarioController.ModelState.AddModelError("Cpf", "Invalid data.");

            var result = await mockUsuarioController.CreateUsuario(usuario);

            var actionResult = Assert.IsType<ActionResult<CreateUsuarioDto>>(result);
            var badRequest = Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            Assert.Equal(400, badRequest.StatusCode); // retorna 400 bad request
        }

        [Fact(DisplayName = "GET /api/usuario/id - deve retornar OK com Admin")]
        public async Task GetById_AdminRole_ReturnOk()
        {
            long usuarioId = 1;
            var usuario = new DetailUsuarioDto
            { 
                Id = usuarioId,
                NomeCompleto = "abc",
                Cpf = "11111111111",
                Email = "admin@teste.com"
            };

            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(m => m.GetByIdAsync(usuarioId)).ReturnsAsync(usuario);
            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, UserRole.Admin.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email)
            }, "mock"));

            mockUsuarioController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user,
                }
            };

            var actionResult = await mockUsuarioController.GetUsuarioById(usuarioId);

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedUsuario = Assert.IsType<DetailUsuarioDto>(okResult.Value);
            Assert.Equal(usuarioId, returnedUsuario.Id);
        }

        [Fact(DisplayName = "GET /api/usuario/id - deve retornar OK com User acessando seus próprios dados")]
        public async Task GetUsuarioById_UserRole_AccessOwnData_ReturnsOk()
        {
            // Arrange
            var usuarioId = 1L;
            var userName = "Rafael";
            var userEmail = "user@test.com";
            var userCpf = "11111111111";
            var usuarioDto = new DetailUsuarioDto 
            { 
                Id = usuarioId, 
                NomeCompleto = userName,
                Email = userEmail,
                Cpf = userCpf,
            };

            var usuarioServiceMock = new Mock<IUsuarioService>();
            usuarioServiceMock.Setup(s => s.GetByIdAsync(usuarioId))
                              .ReturnsAsync(usuarioDto);

            var controller = new UsuarioController(usuarioServiceMock.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, UserRole.User.ToString()),
                new Claim(ClaimTypes.Email, userEmail)
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext 
                {
                    User = user 
                }
            };

            // Act
            var result = await controller.GetUsuarioById(usuarioId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUsuario = Assert.IsType<DetailUsuarioDto>(okResult.Value);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact(DisplayName = "GET /api/usuario/id - deve retornar FORBIDDEN com User acessando dados de outros usuários")]
        public async Task GetUsuarioById_UserRole_AccessOtherUser_ThrowsForbiddenException()
        {
            // Arrange
            var usuarioId = 1L;
            var userName = "Rafael";
            var userEmail = "user@test.com";
            var userCpf = "11111111111";
            var usuarioDto = new DetailUsuarioDto
            {
                Id = usuarioId,
                NomeCompleto = userName,
                Email = userEmail,
                Cpf = userCpf,
            };

            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(s => s.GetByIdAsync(usuarioId))
                              .ReturnsAsync(usuarioDto);

            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, UserRole.User.ToString()),
                new Claim(ClaimTypes.Email, "other@test.com")
            }, "mock"));

            mockUsuarioController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext 
                { 
                    User = user 
                }
            };

            // Act & Assert
            await Assert.ThrowsAsync<ForbiddenException>(() => mockUsuarioController.GetUsuarioById(usuarioId));
        }

        [Fact(DisplayName = "GET /api/usuario - deve retornar OK com Admin")]
        public async Task GetUsuarios_AdminRole_ReturnsOk()
        {
            long usuarioId = 1;
            var usuario = new DetailUsuarioDto
            {
                Id = usuarioId,
                NomeCompleto = "abc",
                Cpf = "11111111111",
                Email = "admin@teste.com"
            };

            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(m => m.GetByIdAsync(usuarioId)).ReturnsAsync(usuario);
            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Role, UserRole.Admin.ToString())
            ], "mock"));

            mockUsuarioController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user,
                }
            };

            var usuarios = new List<DetailUsuarioDto>
            {
                new() { Id = 2, NomeCompleto = "rafael", Cpf = "12345666666", Email = "rafael@email.com", Senha = "Saysay@2025!@", Tipo = UserType.COMUM, Role = UserRole.User, Carteira = 1000, CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow, IsActive = true },
                new() { Id = 3, NomeCompleto = "esbenildo", Cpf = "11112222233", Email = "esbenildo@email.com", Senha = "Saysay@2025!@", Tipo = UserType.COMUM, Role = UserRole.User, Carteira = 1000, CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow, IsActive = true },
                new() { Id = 4, NomeCompleto = "avestruz", Cpf = "11223344556", Email = "avestruz@email.com", Senha = "Saysay@2025!@", Tipo = UserType.COMUM, Role = UserRole.User, Carteira = 1000, CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow, IsActive = true }
            };

            mockUsuarioService.Setup(m => m.GetUsuariosAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<UserType?>(),
                    It.IsAny<UserRole?>(),
                    It.IsAny<bool?>(),
                    It.IsAny<int>(),
                    It.IsAny<int>())).ReturnsAsync(usuarios);

            var actionResult = await mockUsuarioController.GetAllUsuarios(new QueryUsuarioParameter());

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedList = Assert.IsAssignableFrom<IEnumerable<DetailUsuarioDto>>(okResult.Value);

            returnedList.Should().BeEquivalentTo(usuarios, opt => opt.WithStrictOrdering());
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact(DisplayName = "GET /api/usuario - deve retornar FORBIDDEN com User")]
        public async Task GetUsuarios_UserRole_ThrowsForbiddenException()
        {
            // Arrange
            long usuarioId = 1L;
            var userName = "Rafael";
            var userEmail = "user@test.com";
            var userCpf = "11111111111";
            var usuarioDto = new DetailUsuarioDto
            {
                Id = usuarioId,
                NomeCompleto = userName,
                Email = userEmail,
                Cpf = userCpf,
            };

            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(s => s.GetByIdAsync(usuarioId))
                              .ReturnsAsync(usuarioDto);

            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Role, UserRole.User.ToString()),
                new Claim(ClaimTypes.Email, userEmail)
            ], "mock"));

            mockUsuarioController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            var query = new QueryUsuarioParameter();
            var ex = await Assert.ThrowsAsync<ForbiddenException>(() => mockUsuarioController.GetAllUsuarios(query));
           
            Assert.Equal("Only Admin has access to this action", ex.Message);

            mockUsuarioService.Verify(m => m.GetUsuariosAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<UserType?>(),
                    It.IsAny<UserRole?>(),
                    It.IsAny<bool?>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()), Times.Never());
        }

        [Fact(DisplayName = "PUT /api/usuario/id - retorna 200 OK com DTO atualizado (Admin)")]
        public async Task UpdateUsuario_Admin_ReturnsOkWithUpdatedDto()
        {
            long usuarioId = 1;

            var updateDto = new UpdateUsuarioDto
            {
                Id = usuarioId,
                NomeCompleto = "abc",
                Cpf = "11111111111",
                Email = "admin@teste.com"
            };

            // Valores que o serviço vai aplicar (simulação da atualização)
            var novoNome = "abc atualizado";
            var novoEmail = "admin@teste.com"; // pode manter, se não muda
            var novoCpf = "22222222222";       // exemplo: serviço normaliza/atualiza

            var mockUsuarioService = new Mock<IUsuarioService>();

            mockUsuarioService.Setup(s => s.UpdateAsync(It.Is<UpdateUsuarioDto>(d => d.Id == usuarioId))).Callback<UpdateUsuarioDto>(d =>
                {
                    d.NomeCompleto = novoNome;
                    d.Email = novoEmail;
                    d.Cpf = novoCpf;
                })
                .Returns(Task.CompletedTask);

            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Role, UserRole.Admin.ToString()),
                new Claim(ClaimTypes.Email, updateDto.Email)
            ], "mock"));

            mockUsuarioController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var actionResult = await mockUsuarioController.UpdateUsuario(usuarioId, updateDto);

            var ok = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);

            var body = Assert.IsType<UpdateUsuarioDto>(ok.Value);
            Assert.Equal(usuarioId, body.Id);
            Assert.Equal(novoNome, body.NomeCompleto);
            Assert.Equal(novoEmail, body.Email);
            Assert.Equal(novoCpf, body.Cpf);

            mockUsuarioService.Verify(s => s.UpdateAsync(It.Is<UpdateUsuarioDto>(d => d.Id == usuarioId)), Times.Once);
        }

        [Fact(DisplayName = "PUT /api/usuario/id - User atualiza o próprio perfil (200 OK)")]
        public async Task UpdateUsuario_User_OwnProfile_ReturnsOk()
        {
            long usuarioId = 10;
            var updateDto = new UpdateUsuarioDto
            {
                Id = usuarioId,
                NomeCompleto = "abc",
                Cpf = "11111111111",
                Email = "admin@teste.com"
            };

            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(s => s.UpdateAsync(It.IsAny<UpdateUsuarioDto>()))
                       .Callback<UpdateUsuarioDto>(d => d.NomeCompleto = "Novo Nome Confirmado")
                       .Returns(Task.CompletedTask);

            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, UserRole.User.ToString()),
                new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString()),
                new Claim(ClaimTypes.Email, updateDto.Email)
            }, "mock"));

            mockUsuarioController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var result = await mockUsuarioController.UpdateUsuario(usuarioId, updateDto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var body = Assert.IsType<UpdateUsuarioDto>(ok.Value);
            Assert.Equal("Novo Nome Confirmado", body.NomeCompleto);
        }

        [Fact(DisplayName = "PUT /api/usuario/id - User não pode atualizar outro usuário (403)")]
        public async Task UpdateUsuario_User_OtherProfile_Forbidden()
        {
            long routeId = 10; // id na rota
            var updateDto = new UpdateUsuarioDto
            {
                Id = 99,
                NomeCompleto = "abc",
                Cpf = "11111111111",
                Email = "admin@teste.com"
            };

            var mockUsuarioService = new Mock<IUsuarioService>();
            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, UserRole.User.ToString()),
                new Claim(ClaimTypes.NameIdentifier, routeId.ToString()),
                new Claim(ClaimTypes.Email, "user@teste.com")
            }, "mock"));

            mockUsuarioController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var ex = await Assert.ThrowsAsync<BadRequestException>(() => mockUsuarioController.UpdateUsuario(routeId, updateDto));
            Assert.Equal("Different id for operation.", ex.Message);

            mockUsuarioService.Verify(s => s.UpdateAsync(It.IsAny<UpdateUsuarioDto>()), Times.Never);
        }

        [Fact(DisplayName = "DELETE /api/usuario/id - 200 OK com usuário desativado")]
        public async Task DeleteUsuario_Admin_ReturnsOkWithDeactivatedUser()
        {
            // Arrange
            long id = 5;

            var ativo = new DetailUsuarioDto
            {
                Id = id,
                NomeCompleto = "Fulano",
                Email = "fulano@teste.com",
                Cpf = "11111111111",
                IsActive = true
            };

            var desativado = new DetailUsuarioDto
            {
                Id = id,
                NomeCompleto = "Fulano",
                Email = "fulano@teste.com",
                Cpf = "11111111111",
                IsActive = false
            };

            var mockUsuarioService = new Mock<IUsuarioService>();
            mockUsuarioService.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(ativo);
            mockUsuarioService.Setup(s => s.DeleteAsync(id)).Returns(Task.CompletedTask);
            mockUsuarioService.SetupSequence(s => s.GetByIdAsync(id)).ReturnsAsync(ativo).ReturnsAsync(desativado); // segunda consulta (depois do soft delete)

            var mockUsuarioController = new UsuarioController(mockUsuarioService.Object);

            var admin = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Role, UserRole.Admin.ToString()),
                new Claim(ClaimTypes.Email, "admin@teste.com")
            ], "mock"));

            mockUsuarioController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = admin }
            };

            // Act
            var result = await mockUsuarioController.DeleteUsuario(id);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var body = Assert.IsType<DetailUsuarioDto>(ok.Value);
            Assert.False(body.IsActive);

            mockUsuarioService.Verify(s => s.GetByIdAsync(id), Times.Once);
            mockUsuarioService.Verify(s => s.DeleteAsync(id), Times.Once);
        }

    }
}
