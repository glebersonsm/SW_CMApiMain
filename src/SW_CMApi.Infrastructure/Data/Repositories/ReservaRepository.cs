using SW_CMApi.Application.Interfaces.Base;
using SW_CMApi.Domain.Entities;
using SW_CMApi.Domain.Repositories;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using SW_CMApi.Infrastructure.Data.Repositories.Base;

namespace SW_CMApi.Infrastructure.Data.Repositories;

public class ReservaRepository : RepositoryNH, IReservaRepository
{
    public ReservaRepository(ISession session) : base(session)
    {
    }

    public async Task AddAsync(Reserva reserva)
    {
        await Save(reserva);
    }

    public async Task<Reserva?> GetByIdAsync(long id)
    {
        return await FindById<Reserva>(id);
    }

    public async Task<Reserva?> GetByNumeroReservaAsync(long numeroReserva)
    {
        // Exemplo usando LINQ to NHibernate
        return await Session.Query<Reserva>()
                            .FirstOrDefaultAsync(r => r.NumeroReserva == numeroReserva);
    }

    public async Task SaveChangesAsync()
    {
        await Session.FlushAsync();
    }

    public async Task UpdateAsync(Reserva reserva)
    {
        await Save(reserva);
    }
}
