using DesafioBackEnd.API.Application.Dto.Model;
using Microsoft.AspNetCore.Identity;

namespace DesafioBackEnd.API.Domain.Account.Interface
{
    public interface IAuthenticate
    {
        Task<UserToken> AuthenticateAsync(string email, string password);
        Task<bool> RegisterUser(string email, string password);
        Task Logout();
    }
}
