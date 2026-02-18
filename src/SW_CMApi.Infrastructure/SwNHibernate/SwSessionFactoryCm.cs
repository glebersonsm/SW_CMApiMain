using NHibernate;
using SW_CMApi.Application.Interfaces;

namespace SW_CMApi.Infrastructure.SwNHibernate;

/// <summary>
/// Implementação do SessionFactory para o banco CM (Reservas).
/// Padrão alinhado ao projeto Backend - Reservas CM .NET (Beach Park).
/// </summary>
public class SwSessionFactoryCm : ISwSessionFactoryCm
{
    public ISessionFactory? SessionFactory { get; set; }

    public ISession OpenSession()
    {
        return SessionFactory!.OpenSession();
    }
}
