using AutoMapper;
using DesafioBackEnd.API.Application.Command.Usuarios;
using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Service.Interfaces;
using MediatR;
using DesafioBackEnd.API.Domain.Errors;

namespace DesafioBackEnd.API.Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UsuarioService(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task AddAsync(CreateUsuarioDto createUsuarioDto)
        {
            if (createUsuarioDto.NomeCompleto == null)
                throw new BadRequestException("TESTE");

            var usuarioCreateCommand = _mapper.Map<CreateUsuarioDto, UsuarioCreateCommand>(createUsuarioDto);
            await _mediator.Send(usuarioCreateCommand);
        }

        public async Task DeleteAsync(long? id)
        {
            var productDeleteCommand = new UsuarioDeleteCommand(id.Value);
            if (productDeleteCommand == null)
                throw new Exception($"Entity could not be found");

            await _mediator.Send(productDeleteCommand);
        }

        public async Task<DetailUsuarioDto> GetByIdAsync(long? id)
        {
            var usuario = new GetUsuarioByIdQuery(id.Value);
            if (usuario == null)
                throw new Exception($"Entity could not be found");

            var result = await _mediator.Send(usuario);
            return _mapper.Map<DetailUsuarioDto>(result);
        }
            

        public async Task<IEnumerable<DetailUsuarioDto>> GetUsuariosAsync()
        {
            var usuariosQuery = new GetUsuariosQuery();
            if (usuariosQuery == null)
                throw new Exception($"Entities could not be loaded");

            var result = await _mediator.Send(usuariosQuery);
            return _mapper.Map<IEnumerable<DetailUsuarioDto>>(result);
        }

        public async Task UpdateAsync(UpdateUsuarioDto updateUsuarioDto)
        {
            var usuarioUpdateCommand = _mapper.Map<UpdateUsuarioDto, UsuarioUpdateCommand>(updateUsuarioDto);
            await _mediator.Send(usuarioUpdateCommand);
        }
    }
}
