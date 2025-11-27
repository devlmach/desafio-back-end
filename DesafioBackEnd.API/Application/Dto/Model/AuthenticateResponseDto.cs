namespace DesafioBackEnd.API.Application.Dto.Model
{
    public class AuthenticateResponseDto
    {
        public required string Id { get; set; }
        public required IList<string> Roles { get; set; }
    }
}
