using AutoMapper;
using DesafioBackEnd.API.Application.Command.Queries;
using DesafioBackEnd.API.Application.Command.Usuarios;
using DesafioBackEnd.API.Application.Dto.Usuarios;
using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Data.Context;
using DesafioBackEnd.API.Domain.Account.Interface;
using DesafioBackEnd.API.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace DesafioBackEnd.API.Application.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IAuthenticate _authenticate;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioService(IMapper mapper, IMediator mediator, IAuthenticate authenticate, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _mediator = mediator;
            _authenticate = authenticate;
            _userManager = userManager;
        }

        public async Task AddAsync(CreateUsuarioDto createUsuarioDto)
        {
            var usuarioCreateCommand = _mapper.Map<CreateUsuarioDto, UsuarioCreateCommand>(createUsuarioDto);
            await _mediator.Send(usuarioCreateCommand);

            var user = new ApplicationUser
            {
                UserName = createUsuarioDto.Email,
                Email = createUsuarioDto.Email
            };

            var result = await _userManager.CreateAsync(user, createUsuarioDto.Senha);

            if (!result.Succeeded)
                throw new Exception(string.Join($"{result.Errors.Select(e => e.Description)}, "));

            await _userManager.AddToRoleAsync(user, createUsuarioDto.Role.ToString());
        }

        public async Task DeleteAsync(long? id)
        {
            var productDeleteCommand = new UsuarioDeleteCommand(id!.Value);
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
            

        public async Task<IEnumerable<DetailUsuarioDto>> GetUsuariosAsync(string? nomeCompleto, string? cpf, string? email, UserType? tipo, UserRole? role, bool? isActive, int pageNumber, int pageSize)
        {
            var usuariosQuery = new GetUsuariosQuery
            {
                NomeCompleto = nomeCompleto,
                Cpf = cpf,
                Email = email,
                Tipo = tipo,
                Role = role,
                IsActive = isActive,
                PageSize = pageSize,
                PageNumber = pageNumber
                
            };
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
