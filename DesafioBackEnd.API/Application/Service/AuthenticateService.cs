using DesafioBackEnd.API.Application.Dto.Model;
using DesafioBackEnd.API.Data.Context;
using DesafioBackEnd.API.Domain.Account.Interface;
using DesafioBackEnd.API.Domain.Entity;
using DesafioBackEnd.API.Domain.Errors;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DesafioBackEnd.API.Application.Service
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;


        public AuthenticateService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<UserToken> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new BadRequestException("Usuário ou senha inválidos");

            var loginResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            Console.WriteLine(user);
            Console.WriteLine(password);

            if (!loginResult.Succeeded)
                throw new BadRequestException("Usuário ou senha inválidos aqui");
            
            return await GenerateTokenAsync(user);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = email,
                Email = email,
            };

            var result = await _userManager.CreateAsync(applicationUser, password);
            
            if (result.Succeeded)
                await _signInManager.SignInAsync(applicationUser, false);

            return result.Succeeded;
        }

        private async Task<UserToken> GenerateTokenAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userData = new UserLoginData { Id = user.Id, Roles = roles };
            var userDataJson = System.Text.Json.JsonSerializer.Serialize(userData);

            IEnumerable<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.UserData, userDataJson)
            ];

            var usuario = await _dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.Email == user.Email);
            claims = claims.Append(new Claim("UserId", usuario!.Id.ToString()));


            foreach (var role in roles)
            {
                claims = claims.Append(new Claim(ClaimTypes.Role, role));
            }

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
