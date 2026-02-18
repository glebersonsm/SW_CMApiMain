using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NHibernate;
using SW_CMApi.Application.Interfaces;
using SW_CMApi.Domain.Repositories.Base;

namespace SW_CMApi.Infrastructure.Data.Repositories.Core;

/// <summary>
/// Repositório base NHibernate para o banco CM (Reservas).
/// Padrão alinhado ao projeto Backend - Reservas CM .NET (Beach Park).
/// </summary>
public class RepositoryNH : IRepositoryNH
{
    private readonly IUnitOfWorkNHCm _unitOfWork;

    public RepositoryNH(IUnitOfWorkNHCm unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public ISession Session => _unitOfWork.Session;

    public async Task<T> FindById<T>(object id)
    {
        return await Session.GetAsync<T>(id);
    }

    public async Task<T> Save<T>(T entity)
    {
        await Session.SaveOrUpdateAsync(entity);
        return entity;
    }

    public async Task<IList<T>> SaveRange<T>(IList<T> entities)
    {
        foreach (var entity in entities)
        {
            await Session.SaveOrUpdateAsync(entity);
        }
        return entities;
    }

    public async Task Remove<T>(T entity)
    {
        await Session.DeleteAsync(entity);
    }

    public async Task RemoveRange<T>(IList<T> entities)
    {
        foreach (var entity in entities)
        {
            await Session.DeleteAsync(entity);
        }
    }
}
