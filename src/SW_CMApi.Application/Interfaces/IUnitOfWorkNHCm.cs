using NHibernate;

namespace SW_CMApi.Application.Interfaces;

/// <summary>
/// Unit of Work para o banco CM (Reservas).
/// Padrão alinhado ao projeto Backend - Reservas CM .NET (Beach Park).
/// </summary>
public interface IUnitOfWorkNHCm
{
    ISession Session { get; }
    void BeginTransaction();
    Task CommitAsync();
    void Rollback();
    CancellationToken CancellationToken { get; }
}
