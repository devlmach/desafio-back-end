using AutoMapper;
using DesafioBackEnd.API.Application.Command.Queries;
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

        [Fact(DisplayName = "Delete user with IsActive equals false")]
        public async Task DeleteUsuario_WithNoError_ResultCreated()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var mockMapper = new Mock<IMapper>();
            var mockAuthenticate = new Mock<IAuthenticate>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                store.Object, null!, null!, null!, null!, null!, null!, null!, null!
            );

            var usuarioDto = new DetailUsuarioDto
            {
                Id = 1,
                NomeCompleto = "Teste",
                Cpf = "12345678901",
                Email = "teste@teste.com",
                Senha = "@Jaqrafa@2206@#%",
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                Carteira = 1000,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            mockMediator.Setup(m => m.Send(It.IsAny<UsuarioDeleteCommand>(), default)).ReturnsAsync(new Usuario());

            var service = new UsuarioService(mockMapper.Object, mockMediator.Object, mockAuthenticate.Object, mockUserManager.Object);

            // Act
            await service.DeleteAsync(1);

            // Assert
            mockMediator.Verify(m => m.Send(It.IsAny<UsuarioDeleteCommand>(), default), Times.Once);
        }


        [Fact(DisplayName = "Return list of users")]
        public async Task ReturnUsuarios_WithNoError_ResultList()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var mockMapper = new Mock<IMapper>();
            var mockAuthenticate = new Mock<IAuthenticate>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                store.Object, null!, null!, null!, null!, null!, null!, null!, null!
            );

            var users = new List<DetailUsuarioDto>();

            var usuario1 = new DetailUsuarioDto
            {
                Id = 1,
                NomeCompleto = "Teste",
                Cpf = "12345678901",
                Email = "teste@teste.com",
                Senha = "@Jaqrafa@2206@#%",
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                Carteira = 1000,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var usuario2 = new DetailUsuarioDto
            {
                Id = 2,
                NomeCompleto = "Teste",
                Cpf = "12345678901",
                Email = "teste@teste.com",
                Senha = "@Jaqrafa@2236@#%",
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                Carteira = 1400,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            users.Add(usuario1);
            users.Add(usuario2);
            
            mockMediator.Setup(m => m.Send(It.IsAny<GetUsuariosQuery>(), default))
                        .ReturnsAsync(users);

            mockMapper.Setup(m => m.Map<IEnumerable<DetailUsuarioDto>>(users))
                      .Returns(users.ToList);

            var service = new UsuarioService(mockMapper.Object, mockMediator.Object, mockAuthenticate.Object, mockUserManager.Object);

            // Act
            var result = await service.GetUsuariosAsync(null, null, null, null, null, null, 1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            mockMediator.Verify(m => m.Send(It.IsAny<GetUsuariosQuery>(), default), Times.Once);
        }


    }

}
