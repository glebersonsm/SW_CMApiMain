using SW_CMApi.Application.Interfaces;
using SW_CMApi.Domain.Entities;
using SW_CMApi.Domain.Repositories;
using SW_CMApi.Infrastructure.Data.Repositories.Core;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace SW_CMApi.Infrastructure.Data.Repositories;

public class ReservaRepository : RepositoryNH, IReservaRepository
{
    public ReservaRepository(IUnitOfWorkNHCm unitOfWork) : base(unitOfWork)
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
