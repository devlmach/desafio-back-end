using AutoMapper;
using DesafioBackEnd.API.Application.Command.Transacoes;
using DesafioBackEnd.API.Application.Command.Usuarios;
using DesafioBackEnd.API.Application.Dto.Transacoes;
using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Domain.Entity;

namespace DesafioBackEnd.API.Application.Mapping
{
    public class DomainToDTOProfile : Profile
    {
        public DomainToDTOProfile()
        {
            CreateMap<Usuario, CreateUsuarioDto>().ReverseMap();
            CreateMap<CreateUsuarioDto, UsuarioCreateCommand>().ReverseMap();
            CreateMap<Usuario, DetailUsuarioDto>().ReverseMap();
            CreateMap<UpdateUsuarioDto, UsuarioUpdateCommand>().ReverseMap();

            CreateMap<Transacao, CreateTransacaoDto>().ReverseMap();
            CreateMap<CreateTransacaoDto, TransacaoCreateCommand>().ReverseMap();
            CreateMap<Transacao, DetailTransacaoDto>().ReverseMap();

        }
    }
}
