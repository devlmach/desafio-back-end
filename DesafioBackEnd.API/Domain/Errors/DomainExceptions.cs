namespace DesafioBackEnd.API.Domain.Errors
{
    public class DomainExceptions : Exception
    {
        public DomainExceptions(string message) : base(message) { } 
    }

    public class NotFoundException : DomainExceptions
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class BadRequestException : DomainExceptions
    {
        public BadRequestException(string message) : base(message) { }
    }

    public class ForbiddenException : DomainExceptions
    {
        public ForbiddenException(string messase) : base(messase) { }
    }
}
