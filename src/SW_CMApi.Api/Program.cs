using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SW_CMApi.Application.Services.Reservas.Interfaces;
using SW_CMApi.Domain.Repositories.Base;
using SW_CMApi.Application.Services.Reservas;
using SW_CMApi.Domain.Repositories;
using SW_CMApi.Infrastructure.Data.Mappings;
using SW_CMApi.Infrastructure.Data.Repositories;
using SW_CMApi.Infrastructure.Data.Repositories.Base;
using ISession = NHibernate.ISession;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

// NHibernate Configuration
var connectionString = builder.Configuration.GetConnectionString("CmConnection") 
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'CmConnection' or 'DefaultConnection' not found.");
}

IPersistenceConfigurer databaseConfig;

// Simple detection strategy based on common keywords
if (connectionString.Contains("DESCRIPTION") || connectionString.Contains("CONNECT_DATA"))
{
    // Oracle
    databaseConfig = OracleManagedDataClientConfiguration.Oracle10
        .ConnectionString(connectionString)
        .ShowSql()
        .FormatSql()
        .AdoNetBatchSize(50)
        .Raw("throw_on_error", "true");
}
else
{
    // Fallback/Default to MSSQL
    databaseConfig = MsSqlConfiguration.MsSql2012
        .ConnectionString(connectionString)
        .ShowSql();
}

var sessionFactory = Fluently.Configure()
    .Database(databaseConfig)
    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ReservaMap>())
    .BuildSessionFactory();

// Registrar ISessionFactory como Singleton (Thread-safe e custoso para criar)
builder.Services.AddSingleton(sessionFactory);

// Registrar ISession como Scoped (Uma por requisição)
builder.Services.AddScoped<ISession>(factory =>
{
    return factory.GetService<ISessionFactory>().OpenSession();
});

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
