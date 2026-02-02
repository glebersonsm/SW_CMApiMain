using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SW_CMApi.Application.Interfaces;
using SW_CMApi.Domain.Repositories.Base;
using SW_CMApi.Application.Services;
using SW_CMApi.Domain.Repositories;
using SW_CMApi.Infrastructure.Data.Mappings;
using SW_CMApi.Infrastructure.Data.Repositories;
using SW_CMApi.Infrastructure.Data.Repositories.Base;
using ISession = NHibernate.ISession;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// NHibernate Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var sessionFactory = Fluently.Configure()
    .Database(MsSqlConfiguration.MsSql2012
        .ConnectionString(connectionString)
        .ShowSql() // Útil para debug
    )
    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ReservaMap>())
    // .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false)) // Cuidado em produção!
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
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
