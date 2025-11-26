using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace DesafioBackEnd.API.IoC
{
    public static class InfraDbJWT
    {
        public static IServiceCollection AddInfrastructureJWT(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // DEIFNIR ATIVAÇÃO E VALIDAÇÃO DO TOKEN JWT

            // 1 TERAFA - informar o tipo de autenticação - JWT BEARER
            // 2 TAREFA - definir o modelo de desafio de autenticação
            serviceCollection.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // HABILITA A AUTENTICAÇÃO JWT USANDO OESQUEMA E DESAFIO DEFINIDOS
            // VALIDA TOKEN

            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // oq quero validar
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    // fazer validação
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                    RoleClaimType = ClaimTypes.Role,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return serviceCollection;
        }
    }
}
