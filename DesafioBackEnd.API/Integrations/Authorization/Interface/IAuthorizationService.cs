namespace DesafioBackEnd.API.Integrations.Authorization.Interface
{
    public interface IAuthorizationService
    {
        Task<bool> IsAuthorizedAsync();
    }
}
