using NHibernate;
using SW_CMApi.Application.Interfaces;

namespace SW_CMApi.Infrastructure.Data.Repositories.Core;

/// <summary>
/// Unit of Work para o banco CM (Reservas).
/// Padrão alinhado ao projeto Backend - Reservas CM .NET (Beach Park).
/// </summary>
public class UnitOfWorkNHCm : IUnitOfWorkNHCm, IDisposable
{
    public ISession Session { get; private set; }
    private ITransaction? _transaction;
    private readonly CancellationToken _cancellationToken;
    public CancellationToken CancellationToken => _cancellationToken;

    private readonly ISwSessionFactoryCm _sessionFactory;

    public UnitOfWorkNHCm(ISwSessionFactoryCm sessionFactory)
    {
        _sessionFactory = sessionFactory ?? throw new ArgumentNullException(nameof(sessionFactory));
        Session = _sessionFactory.OpenSession();
        _cancellationToken = new CancellationToken();
    }

    public void BeginTransaction()
    {
        var currentTransaction = Session.GetCurrentTransaction();
        if (currentTransaction == null || !currentTransaction.IsActive)
        {
            _transaction = Session.BeginTransaction();
        }
    }

    public async Task CommitAsync()
    {
        var currentTransaction = Session.GetCurrentTransaction();
        if (currentTransaction != null && currentTransaction.IsActive && !currentTransaction.WasCommitted)
        {
            await currentTransaction.CommitAsync();
        }
    }

    public void Rollback()
    {
        var currentTransaction = Session.GetCurrentTransaction();
        if (currentTransaction != null && currentTransaction.IsActive && !currentTransaction.WasRolledBack)
        {
            currentTransaction.Rollback();
        }
    }

    public void Dispose()
    {
        Session?.Dispose();
    }
}
