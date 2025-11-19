using DesafioBackEnd.API.Application.Mapping;
using DesafioBackEnd.API.Common.Middleware;
using DesafioBackEnd.API.IoC;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(opt =>
    {
        opt.InvalidModelStateResponseFactory = (ctx) =>
        {
            var errors = ctx.ModelState
                .Where(e => e.Value?.Errors.Count > 0)
                .Select(e => new ErrorDetailResponse
                {
                    Field = e.Key,
                    Message = e.Value?.Errors.First().ErrorMessage
                });

            var customResponse = new ErrorResponse
            {
                Message = "A requisição não respeita a semântica do objeto.",
                StatusCode = StatusCodes.Status400BadRequest,
                Details = errors
            };

            return new BadRequestObjectResult(customResponse);
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xml = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddAutoMapper(cfg => { }, typeof(DomainToDTOProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ExceptionsMiddeware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
