using DesafioBackEnd.API.Integrations.Authorization.Interface;

namespace DesafioBackEnd.API.Integrations.Authorization.Service
{
    public class ExternalAuthorizationService : IAuthorizationService
    {
        private readonly HttpClient _httpClient;

        public ExternalAuthorizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsAuthorizedAsync()
        {
            var response = await _httpClient.GetAsync("https://util.devi.tools/api/v2/authorize");

            var autorizacao = await response.Content.ReadAsStringAsync();

            var autorizacaoTrue = System.Text.Json.JsonSerializer.Deserialize<AuthorizationResponse>(autorizacao);

            if (autorizacaoTrue.data.authorization == false)
                throw new Exception("Denied transaction");

            return autorizacaoTrue.data.authorization;

        }
    }
}
