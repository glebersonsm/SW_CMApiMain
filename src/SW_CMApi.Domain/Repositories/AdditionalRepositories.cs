using SW_CMApi.Domain.Repositories.Base;
using SW_CMApi.Domain.Entities;
using System.Threading.Tasks;

namespace SW_CMApi.Domain.Repositories;

public interface IParametroHotelRepository : IRepositoryNH
{
    Task<ParametroHotel?> GetByIdHotelAsync(long idHotel);
}

public interface IHospedeRepository : IRepositoryNH
{
    // Custom methods if needed
}

public interface IMovimentoHospedeRepository : IRepositoryNH
{
    // Custom methods if needed
}

public interface IReservaReduzidaRepository : IRepositoryNH
{
     Task AddAsync(ReservaReduzida reservaReduzida);
}
