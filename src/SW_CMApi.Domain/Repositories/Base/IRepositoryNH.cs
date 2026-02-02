using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;

namespace SW_CMApi.Domain.Repositories.Base;

public interface IRepositoryNH
{
    ISession Session { get; } // Simplificado de StatelessSession para Session normal por enquanto, ou manter Stateless se for o padrão
    // O exemplo usava IStatelessSession, vou manter compativel com o original se possivel, mas Stateless tem limitações (sem lazy load, sem cache 1o nivel).
    // O original usava _unitOfWork.Session que retornava IStatelessSession.
    // Mas também tinha métodos FindEntityWithRelationships que tentava carregar relacionamentos.
    // Vou usar ISession normal para evitar dores de cabeça com LazyLoading a menos que restrito.
    // Porem o "RepositoryNH" original explicitamente tipava: public IStatelessSession? Session => _unitOfWork.Session;
    // Isso é uma decisão arquitetural forte (performance > conveniência).
    // Vou começar com ISession para facilitar, já que Stateless não faz dirty tracking para Update automático.
    
    // CORREÇÃO: Para ser field-compatible com o "Backend - API", devo tentar seguir o padrão.
    // Se eles usam Stateless, eles fazem Save explícito.
    
    Task<T> FindById<T>(object id); // Id pode ser int ou long
    Task<T> Save<T>(T entity);
    Task Remove<T>(T entity);
    // Task<IList<T>> FindByHql<T>(string hql, params object[] parameters); // Simplificando params
}
