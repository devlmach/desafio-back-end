using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Data.Context;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Data.Repository
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        //private readonly IUsuarioService _usuarioService;
        public TransacaoRepository(ApplicationDbContext dbContext, IUsuarioService usuarioService)
        {
            _dbContext = dbContext;
            //_usuarioService = usuarioService;
        }

        public async Task<Transacao> CreateTransacaoAsync(Transacao transacao)
        {
           // var sender = await _usuarioService.GetByIdAsync(transacao.IdSender);
            //var receiver = await _usuarioService.GetByIdAsync(transacao.IdReceiver);

            /*if (sender == null || receiver == null)
                throw new Exception("COMUM ou LOJISTA não encontrado.");

            if (sender.Tipo == UserType.LOJISTA)
                throw new Exception("Usuario do tipo lojista não pode realizar transações.");

            if (sender.Carteira <= 0)
                throw new Exception("Usuário impossibilitado de realizar transferência.");

            if (sender.Carteira < transacao.QuantiaTransferida)
                throw new Exception("Usuário impossibilitado de realizar transferencia pois seu saldo é menor do que a quantia desejada a ser transferida.");*/

            using var request = new HttpClient();
            var response = await request.GetAsync("https://util.devi.tools/api/v2/authorize");
            var autorizacao = await response.Content.ReadAsStringAsync();
            var autorizacaoTrue = System.Text.Json.JsonSerializer.Deserialize<AuthorizationResponse>(autorizacao);

            if (autorizacaoTrue.data.authorization == false)
                throw new Exception("Transação negada.");

            Transacao newTransacao = new Transacao();

            newTransacao.QuantiaTransferida = transacao.QuantiaTransferida;
            newTransacao.IdSender = transacao.IdSender;
            newTransacao.IdReceiver = transacao.IdReceiver;

            //sender.Carteira -= newTransacao.QuantiaTransferida;
            //receiver.Carteira += newTransacao.QuantiaTransferida;

            _dbContext.Transacoes.Add(newTransacao);
            //_dbContext.Usuarios.Update(sender);
            //_dbContext.Usuarios.Update(receiver); 
                
            await _dbContext.SaveChangesAsync();
            return newTransacao;
        }
    }
}
