using DesafioBackEnd.API.Domain.Repository;
using DesafioBackEnd.API.Domain.Repository.Interfaces;
using DesafioBackEnd.API.Domain.Service;
using DesafioBackEnd.API.Domain.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioBackEnd.API.Domain.Context
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
