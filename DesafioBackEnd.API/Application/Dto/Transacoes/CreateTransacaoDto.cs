using System.ComponentModel.DataAnnotations;

namespace DesafioBackEnd.API.Application.Dto.Transacoes
{
    public class CreateTransacaoDto
    {
        //[Required(ErrorMessage = "O ID do pagador não pode ser vazio")]
        //public long? IdSender { get; set; }

        [Required(ErrorMessage = "O ID do recebedor não pode ser vazio")]
        public long? IdReceiver { get; set; }

        [Required(ErrorMessage = "O valor a ser transferido não pode ser vazio")]
        public decimal QuantiaTransferida { get; set; }
    }
}
