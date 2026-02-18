using DotNetEnv;
using Microsoft.OpenApi.Models;
using SW_CMApi.Application.Services.Reservas;
using SW_CMApi.Application.Services.Reservas.Interfaces;
using SW_CMApi.Domain.Repositories;
using SW_CMApi.Domain.Repositories.Base;
using SW_CMApi.Infrastructure.Data.Repositories;
using SW_CMApi.Infrastructure.Data.Repositories.Core;
using SW_CMApi.Infrastructure.Extensions;

// Carrega as variáveis de ambiente do arquivo .env (mesmo padrão do Backend - Reservas CM .NET Beach Park)
var envPath = Path.Combine(AppContext.BaseDirectory, ".env");
if (!File.Exists(envPath))
{
    envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
}
if (!File.Exists(envPath))
{
    // Tenta na raiz da solução (parent do projeto)
    var solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName;
    if (!string.IsNullOrEmpty(solutionRoot))
    {
        envPath = Path.Combine(solutionRoot, ".env");
    }
}

if (File.Exists(envPath))
{
    Console.WriteLine($"Arquivo .env carregado de: {envPath}");
    Env.Load(envPath);
}
else
{
    Console.WriteLine($"AVISO: Arquivo .env não encontrado. Usando appsettings.json.");
    Console.WriteLine($"  - Tentado em: {Path.Combine(AppContext.BaseDirectory, ".env")}");
    Console.WriteLine($"  - Tentado em: {Path.Combine(Directory.GetCurrentDirectory(), ".env")}");
}

var builder = WebApplication.CreateBuilder(args);

// Sobrescreve ConnectionStrings:CmConnection com CM_CONNECTION do .env (se definido)
var cmConnection = Environment.GetEnvironmentVariable("CM_CONNECTION");
if (!string.IsNullOrWhiteSpace(cmConnection))
{
    builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
    {
        ["ConnectionStrings:CmConnection"] = cmConnection
    });
}

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger Configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Backend - API.NET Reservas CM (Beach Park)",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "SW Soluções Integradas Ltda",
            Email = "contato@swsolucoes.inf.br",
            Url = new Uri("https://www.swsolucoes.inf.br")
        }
    });
});

// NHibernate Configuration - Padrão alinhado ao Backend Reservas CM .NET (Beach Park)
builder.Services.AddNHibernateCm(builder.Configuration);

// Registrar Repositories e Services
builder.Services.AddScoped<IRepositoryNH, RepositoryNH>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<IParametroHotelRepository, ParametroHotelRepository>();
builder.Services.AddScoped<IHospedeRepository, HospedeRepository>();
builder.Services.AddScoped<IMovimentoHospedeRepository, MovimentoHospedeRepository>();
builder.Services.AddScoped<IReservaReduzidaRepository, ReservaReduzidaRepository>();

builder.Services.AddScoped<IReservaService, ReservaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend - API.NET Reservas CM (Beach Park) v1");
        c.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
