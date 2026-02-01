using SW_CMApi.Domain.Entities;
using System.Threading.Tasks;

namespace SW_CMApi.Domain.Repositories;

public interface IReservaRepository
{
    Task<Reserva?> GetByIdAsync(long id);
    Task<Reserva?> GetByNumeroReservaAsync(long numeroReserva);
    Task AddAsync(Reserva reserva);
    Task UpdateAsync(Reserva reserva);
    // Task DeleteAsync(long id); // Não tem no Java, mas é comum
    Task SaveChangesAsync();
}
