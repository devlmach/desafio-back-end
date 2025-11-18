namespace DesafioBackEnd.API.Application.Command.Usuarios
{
    public class UsuarioUpdateCommand : UsuarioCommand
    {
        public long Id { get; set; }

        public UsuarioUpdateCommand(long id)
        {
            Id = id;
        }
    }
}
