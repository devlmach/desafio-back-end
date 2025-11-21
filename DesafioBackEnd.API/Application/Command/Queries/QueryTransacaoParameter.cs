namespace DesafioBackEnd.API.Application.Command.Queries
{
    public class QueryTransacaoParameter
    {
        public int PageNumber { get; set; } = 1; // pagina atual
        public int PageSize { get; set; } = 10; // quant. por pagina
    }
}
