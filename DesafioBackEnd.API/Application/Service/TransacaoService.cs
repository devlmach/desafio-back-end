using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Application.Service
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository, IUsuarioRepository usuarioRepository)
        {
            _transacaoRepository = transacaoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Transacao> CreateTransacaoAsync(Transacao transacao)
        {

           // var idSender = await _usuarioRepository.GetByIdAsync(usuario.Id);
            //var idReceiver = await _usuarioRepository.GetByIdAsync(usuario.Id);



            return await _transacaoRepository.CreateTransacaoAsync(transacao);
        }
    }
}
