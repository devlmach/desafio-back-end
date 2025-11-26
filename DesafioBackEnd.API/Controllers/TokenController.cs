using DesafioBackEnd.API.Application.Dto.Model;
using DesafioBackEnd.API.Data.Context;
using DesafioBackEnd.API.Domain.Account.Interface;
using DesafioBackEnd.API.Domain.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;


        public TokenController(IAuthenticate authenticate, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _authenticate = authenticate ?? throw new ArgumentNullException(nameof(authenticate));
            _configuration = configuration;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel loginModel)
        {
            var result = await _authenticate.Authenticate(loginModel.Email, loginModel.Password);

            if (result)
            {
                return GenerateToken(loginModel);
            }
            else
            {
                throw new BadRequestException("aqui");
            }
        }

        private UserToken GenerateToken(LoginModel loginModel)
        {

            var claims = new[]
            {
                new Claim("email", loginModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", loginModel.Role)
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new (
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
