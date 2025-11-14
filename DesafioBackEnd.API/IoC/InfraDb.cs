using DesafioBackEnd.API.Application.Service;
using DesafioBackEnd.API.Application.Service.Interfaces;
using DesafioBackEnd.API.Data.Context;
using DesafioBackEnd.API.Data.Repository;
using DesafioBackEnd.API.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEnd.API.IoC
{
    public static class InfraDb
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<ITransacaoService, TransacaoService>();

            return services;
        }
    }
}
