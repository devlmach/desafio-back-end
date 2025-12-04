using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Application.Dto.Model
{
    public class UserLoginData
    {
        public required long Id { get; set; }
        public required IEnumerable<string> Roles { get; set; }
        public required string Email { get; set; }

        public bool IsAdmin()
        {
            return Roles != null && Roles.Any(a => a.Equals(UserRole.Admin));
        }
    }
}
