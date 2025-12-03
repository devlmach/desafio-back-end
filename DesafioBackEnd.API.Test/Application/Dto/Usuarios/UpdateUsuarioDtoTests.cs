using AutoMapper;
using DesafioBackEnd.API.Application.Command.Handler.Usuarios;
using DesafioBackEnd.API.Application.Command.Usuarios;
using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;
using DesafioBackEnd.API.Domain.Errors;
using FluentAssertions;
using Moq;

namespace DesafioBackEnd.API.Test.Application.Dto.Usuarios
{
    public class UpdateUsuarioDtoTests
    {

        [Fact(DisplayName = "UpdateUsuario with wrong id")]
        public void UpdateUsuario_WrongId_ReturnErrorMessage()
        {
            // Arrange
            var usuario = new UpdateUsuarioDto
            {
                Id = 99, // ID incorreto
                NomeCompleto = "Teste",
                Cpf = "12345678900",
                Email = "teste@email.com",
                Senha = "Senha@123",
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                Carteira = 1000,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            var usuarioExistente = new Usuario
            {
                Id = 1, // ID real no banco
                NomeCompleto = "Original",
                Cpf = "12345678900",
                Email = "original@email.com",
                Senha = "Senha@123",
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                Carteira = 1000,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            var command = new UsuarioUpdateCommand(1)
            {
                Id = usuario.Id,
                NomeCompleto = usuario.NomeCompleto,
                Cpf = usuario.Cpf,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Tipo = usuario.Tipo,
                Role = usuario.Role,
                Carteira = usuario.Carteira,
                CreatedAt = usuario.CreatedAt,
                UpdatedAt = usuario.UpdatedAt,
                IsActive = usuario.IsActive
            };

            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<UsuarioUpdateCommand>(It.IsAny<UpdateUsuarioDto>())).Returns(command);
            mockUsuarioRepository.Setup(r => r.GetByIdAsync(usuario.Id)).ReturnsAsync(usuarioExistente);

            var handler = new UsuarioUpdateCommandHandler(mockUsuarioRepository.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().ThrowAsync<BadRequestException>().WithMessage("Different id for operation.");
        }

        [Fact(DisplayName = "UpdateUsuario with same id")]
        public void UpdateUsuario_SameId_ReturnOk()
        {
            // Arrange
            var usuario = new UpdateUsuarioDto
            {
                Id = 1, // ID correto
                NomeCompleto = "Teste",
                Cpf = "12345678900",
                Email = "teste@email.com",
                Senha = "Senha@123",
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                Carteira = 1000,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            var usuarioExistente = new Usuario
            {
                Id = 1, // ID real no banco
                NomeCompleto = "Original",
                Cpf = "12345678900",
                Email = "original@email.com",
                Senha = "Senha@123",
                Tipo = UserType.COMUM,
                Role = UserRole.User,
                Carteira = 1000,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            var command = new UsuarioUpdateCommand(1)
            {
                Id = usuario.Id,
                NomeCompleto = usuario.NomeCompleto,
                Cpf = usuario.Cpf,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Tipo = usuario.Tipo,
                Role = usuario.Role,
                Carteira = usuario.Carteira,
                CreatedAt = usuario.CreatedAt,
                UpdatedAt = usuario.UpdatedAt,
                IsActive = usuario.IsActive
            };

            var mockUsuarioRepository = new Mock<IUsuarioRepository>();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<UsuarioUpdateCommand>(It.IsAny<UpdateUsuarioDto>())).Returns(command);
            mockUsuarioRepository.Setup(r => r.GetByIdAsync(usuario.Id)).ReturnsAsync(usuarioExistente);

            var handler = new UsuarioUpdateCommandHandler(mockUsuarioRepository.Object);

            // Act
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotThrowAsync<BadRequestException>();
        }

    }
}
