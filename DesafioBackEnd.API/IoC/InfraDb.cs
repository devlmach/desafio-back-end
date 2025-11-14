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

            services.AddAutoMapper(cfg =>
            {
                cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzkzNDA0ODAwIiwiaWF0IjoiMTc2MTkzODg2MSIsImFjY291bnRfaWQiOiIwMTlhM2JiYzJlMDg3Y2NmYjNiZjRjMTUwMGQyNDczZiIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazh4dnR2dmJ5ZnNobTV6NHM2YzFmNXQ1Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.pRsdG8z2G4Iby3YF2HzfcydHJ2Ia7T84-K0OpbIb1KCuHki3KabrbenriBl6MzALMalNWuWCrT-s-WQteyTBAGxtiq2DFR8D9DFIWNqQ8H9r64UbsldKpYjbiLuMWLsCUjRdKlhwKL-kY0mGOgwEcy8OScbgPepD0PQlp1yG2rd0Dg5G5jOmhGIU2R9W2y_YwGNp7F7uf_SoDs82mTdAqpYX6mjIBHn4Y5cNKgB5EzOvxgihwMQIzNbatj0RF_LWJv5cToXbXbOmrvwUgsQ-cN_Qdjo84jB8zPU0KYUV0Pu3unt8f2ofVS89aBc_5RvdszhHYnTfAIf8QUQIckmTsg";
            });

            var handler = AppDomain.CurrentDomain.Load("DesafioBackEnd.API");

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(handler));
            
            return services;
        }
    }
}
