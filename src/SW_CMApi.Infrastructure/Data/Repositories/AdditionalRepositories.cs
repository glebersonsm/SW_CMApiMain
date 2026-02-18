using SW_CMApi.Application.Interfaces;
using SW_CMApi.Domain.Entities;
using SW_CMApi.Domain.Repositories;
using SW_CMApi.Infrastructure.Data.Repositories.Core;
using System.Threading.Tasks;

namespace SW_CMApi.Infrastructure.Data.Repositories;

public class ParametroHotelRepository : RepositoryNH, IParametroHotelRepository
{
    public ParametroHotelRepository(IUnitOfWorkNHCm unitOfWork) : base(unitOfWork) { }

    public async Task<ParametroHotel?> GetByIdHotelAsync(long idHotel)
    {
        return await FindById<ParametroHotel>(idHotel);
    }
}

public class HospedeRepository : RepositoryNH, IHospedeRepository
{
    public HospedeRepository(IUnitOfWorkNHCm unitOfWork) : base(unitOfWork) { }
}

public class MovimentoHospedeRepository : RepositoryNH, IMovimentoHospedeRepository
{
    public MovimentoHospedeRepository(IUnitOfWorkNHCm unitOfWork) : base(unitOfWork) { }
}

public class ReservaReduzidaRepository : RepositoryNH, IReservaReduzidaRepository
{
    public ReservaReduzidaRepository(IUnitOfWorkNHCm unitOfWork) : base(unitOfWork) { }

    public async Task AddAsync(ReservaReduzida reservaReduzida)
    {
        await Save(reservaReduzida);
    }
}
