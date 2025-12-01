using AutoMapper;
using DesafioBackEnd.API.Application.Command.Usuarios;
using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Application.Service;
using DesafioBackEnd.API.Data.Context;
using DesafioBackEnd.API.Domain.Account.Interface;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace DesafioBackEnd.API.Test.Application.Service
{
    public class UsuarioServiceTests
    {
        [Fact(DisplayName = "Create user with no error")]
        public async Task CreateUsuario_WithNoError_ResultCreated()
        {
            //arrange
            var mockMapper = new Mock<IMapper>();
            var mockMediator = new Mock<IMediator>();
            var mockAuthenticate = new Mock<IAuthenticate>();

            //mock do usermanager
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            mockMapper.Setup(m => m.Map<CreateUsuarioDto, UsuarioCreateCommand>(It.IsAny<CreateUsuarioDto>())).Returns(new UsuarioCreateCommand());
            mockMediator.Setup(m => m.Send(It.IsAny<IRequest<Usuario>>(), default)).ReturnsAsync(new Usuario());
            mockUserManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var service = new UsuarioService(mockMapper.Object, mockMediator.Object, mockAuthenticate.Object, mockUserManager.Object);

            var newUsuario = new CreateUsuarioDto
            {
                NomeCompleto = "Rafael Luna",
                Cpf = "48405315870",
                Email = "machiavelli.rafa@gmail.com",
                Senha = "@Jaqrafa@2206@#%",
                Tipo = UserType.COMUM,
                Role = UserRole.User
            };

            await service.AddAsync(newUsuario);
            mockMediator.Verify(m => m.Send(It.IsAny<IRequest<Usuario>>(), default), Times.Once());
            mockUserManager.Verify(u => u.CreateAsync(It.IsAny<ApplicationUser>(), newUsuario.Senha), Times.Once);
            mockUserManager.Verify(u => u.AddToRoleAsync(It.IsAny<ApplicationUser>(), newUsuario.Role.ToString()), Times.Once);
        }
    }
}
