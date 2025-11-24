namespace DesafioBackEnd.API.Application.Dto.Model
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
