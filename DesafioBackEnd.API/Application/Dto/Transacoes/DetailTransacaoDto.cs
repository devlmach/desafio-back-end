namespace DesafioBackEnd.API.Application.Dto.Transacao
{
    public class DetailTransacaoDto
    {
        public long Id { get; set; }

        public long? IdSender { get; set; }

        public long? IdReceiver { get; set; }

        public decimal? QuantiaTransferida { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
