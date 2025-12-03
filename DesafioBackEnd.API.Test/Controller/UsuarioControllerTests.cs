using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Controllers;
using Moq;
using DesafioBackEnd.API.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

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

        [Fact(DisplayName = "POST /api/usuario - deve retornar 400 BadRequest")]
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





    }
}
