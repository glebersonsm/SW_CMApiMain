using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using SW_CMApi.Application.Interfaces.Base;

namespace SW_CMApi.Infrastructure.Data.Repositories.Base;

public class RepositoryNH : IRepositoryNH
{
    private readonly ISession _session;

    public RepositoryNH(ISession session)
    {
        _session = session;
    }

    public ISession Session => _session;

    public async Task<T> FindById<T>(object id)
    {
        return await _session.GetAsync<T>(id);
    }

    public async Task<T> Save<T>(T entity)
    {
        await _session.SaveOrUpdateAsync(entity);
        return entity;
    }

    public async Task Remove<T>(T entity)
    {
         await _session.DeleteAsync(entity);
    }
}
