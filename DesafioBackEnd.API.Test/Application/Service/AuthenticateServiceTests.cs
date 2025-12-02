using DesafioBackEnd.API.Application.Service;
using DesafioBackEnd.API.Data.Context;
using DesafioBackEnd.API.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace DesafioBackEnd.API.Test.Application.Service
{
    public class AuthenticateServiceTests
    {
        [Fact(DisplayName = "Generate valid Token JWT")]
        public async Task AuthenticateAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

            var mockSignInManager = new Mock<SignInManager<ApplicationUser>>(mockUserManager.Object,
                Mock.Of<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), null!, null!, null!, null!);

            var mockConfiguration = new Mock<IConfiguration>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Testdb").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new AuthenticateService(mockUserManager.Object, mockSignInManager.Object, mockConfiguration.Object, dbContext);

            var usuario = new Usuario
            {
                NomeCompleto = "Rafael",
                Cpf = "48405315870",
                Email = "machiavelli.rafa@gmail.com",
                Senha = "@Jaqrafa@2206@#%",
                Tipo = UserType.COMUM,
                Role = UserRole.User
            };
            await dbContext.AddAsync(usuario);

            var user = new ApplicationUser
            {
                Id = 123,
                Email = usuario.Email,
                UserName = usuario.Email
            };

            mockUserManager.Setup(m => m.FindByEmailAsync(usuario.Email)).ReturnsAsync(user);

            mockSignInManager.Setup(m => m.CheckPasswordSignInAsync(user, usuario.Senha, false)).ReturnsAsync(SignInResult.Success);

            var inMemorySettings = new Dictionary<string, string>
            {
                { "Jwt:SecretKey", "abra#cadabra$sim@salabim*2021@2025@3546799sasada@" },
                { "Jwt:Issuer", "TestIssuer" },
                { "Jwt:Audience", "TestAudience" }
            };

            var config = new ConfigurationBuilder().AddInMemoryCollection(inMemorySettings!).Build();

            var token = new JwtSecurityToken(
                issuer: "TestIssuer",
                audience: "TestAudience",
                expires: DateTime.UtcNow.AddMinutes(10)
                );

            Assert.NotNull(token);
        }
    }
}
