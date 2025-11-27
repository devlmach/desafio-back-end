namespace DesafioBackEnd.API.Integrations.Authorization.Interface
{
    public interface IAuthorizationsService
    {
        Task<bool> IsAuthorizedAsync();
    }
}
