using NHibernate;

namespace SW_CMApi.Application.Interfaces;

/// <summary>
/// Interface para o SessionFactory do banco CM (Reservas).
/// Padrão alinhado ao projeto Backend - Reservas CM .NET (Beach Park).
/// </summary>
public interface ISwSessionFactoryCm
{
    ISession OpenSession();
}
