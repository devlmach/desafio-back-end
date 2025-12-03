using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Domain.Entity
{
    public class Transacao : ClassBase
    {
        [Required(ErrorMessage = "O ID do pagador não pode ser vazio")]
        public long? IdSender { get; set; }

        [Required(ErrorMessage = "O ID do recebedor não pode ser vazio")]
        public long? IdReceiver { get; set; }

        [Required(ErrorMessage = "O valor a ser transferido não pode ser vazio")]
        public decimal? QuantiaTransferida { get; set; }

        public Transacao() { }

        public Transacao(long id, long idSender, long idReceiver, decimal? quantiaTransferida, DateTime createdAt, DateTime updatedAt, bool isActive)
        {
            Id = id;
            IdSender = idSender;
            IdReceiver = idReceiver;
            QuantiaTransferida = quantiaTransferida;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsActive = isActive;
        }

        public Transacao(long? idSender, long? idReceiver, decimal? quantiaTransferida, DateTime createdAt)
        {
            IdSender = idSender;
            IdReceiver = idReceiver;
            QuantiaTransferida = quantiaTransferida;
            CreatedAt = createdAt;
        }
    }
}
