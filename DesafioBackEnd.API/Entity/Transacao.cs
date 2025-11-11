using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Entity
{
    public class Transacao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O ID do pagador não pode ser vazio")]
        public int IdSender { get; set; }

        [Required(ErrorMessage = "O ID do recebedor não pode ser vazio")]
        public int IdReceiver { get; set; }

        [Required(ErrorMessage = "O valor a ser transferido não pode ser vazio")]
        public decimal QuantiaTransferida { get; set; }

        public Transacao() { }

        public Transacao(int id, int idSender, int idReceiver, decimal quantiaTransferida)
        {
            Id = id;
            IdSender = idSender;
            IdReceiver = idReceiver;
            QuantiaTransferida = quantiaTransferida;
        }
    }
}
