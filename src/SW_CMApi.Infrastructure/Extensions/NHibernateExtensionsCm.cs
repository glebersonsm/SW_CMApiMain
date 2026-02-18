using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SW_CMApi.Application.Interfaces;
using SW_CMApi.Infrastructure.Data.Mappings;
using SW_CMApi.Infrastructure.Data.Repositories.Core;
using SW_CMApi.Infrastructure.SwNHibernate;

namespace SW_CMApi.Infrastructure.Extensions;

/// <summary>
/// Extensões para configuração do NHibernate no projeto CM Reservas.
/// Padrão alinhado ao projeto Backend - Reservas CM .NET (Beach Park).
/// </summary>
public static class NHibernateExtensionsCm
{
    public static IServiceCollection AddNHibernateCm(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var connectionString = configuration.GetConnectionString("CmConnection")
            ?? configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'CmConnection' or 'DefaultConnection' not found.");
        }

        IPersistenceConfigurer databaseConfig;

        if (connectionString.Contains("DESCRIPTION", StringComparison.OrdinalIgnoreCase)
            || connectionString.Contains("CONNECT_DATA", StringComparison.OrdinalIgnoreCase))
        {
            databaseConfig = OracleManagedDataClientConfiguration.Oracle10
                .ConnectionString(connectionString)
                .ShowSql()
                .FormatSql()
                .AdoNetBatchSize(50)
                .Raw("throw_on_error", "true");
        }
        else
        {
            databaseConfig = MsSqlConfiguration.MsSql2012
                .ConnectionString(connectionString)
                .ShowSql();
        }

        var sessionFactory = Fluently.Configure()
            .Database(databaseConfig)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ReservaMap>())
            .BuildSessionFactory();

        var sf = new SwSessionFactoryCm { SessionFactory = sessionFactory };

        services.TryAddSingleton<ISwSessionFactoryCm>(sf);
        services.TryAddScoped<IUnitOfWorkNHCm, UnitOfWorkNHCm>();

        return services;
    }
}
