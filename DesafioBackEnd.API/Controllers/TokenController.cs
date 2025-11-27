using DesafioBackEnd.API.Application.Dto.Model;
using DesafioBackEnd.API.Domain.Account.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DesafioBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        //private readonly IConfiguration _configuration;
        //private readonly IUsuarioService _usuarioService;


        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate = authenticate ?? throw new ArgumentNullException(nameof(authenticate));
            //_configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("loginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel loginModel)
        {
            var result = await _authenticate.AuthenticateAsync(loginModel.Email, loginModel.Password);
            return Ok(result!);
        }

        [HttpGet("me")]
        [Authorize(Roles = "User")]
        public ActionResult GetMe()
        {
            var user = Request.HttpContext.User.Claims
                .First(f => f.Type == ClaimTypes.UserData);

            return Ok(System.Text.Json.JsonSerializer.Deserialize<UserLoginData>(user.Value));
        }

        public static UserLoginData GetUser(HttpRequest request)
        {
            var user = request.HttpContext.User.Claims
                .First(f => f.Type == ClaimTypes.UserData);

            return System.Text.Json.JsonSerializer.Deserialize<UserLoginData>(user.Value)!;
        }
    }
}
