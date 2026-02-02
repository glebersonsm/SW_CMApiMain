using NHibernate;
using SW_CMApi.Domain.Entities;
using SW_CMApi.Domain.Repositories;
using SW_CMApi.Infrastructure.Data.Repositories.Base;
using System.Threading.Tasks;

namespace SW_CMApi.Infrastructure.Data.Repositories;

public class ParametroHotelRepository : RepositoryNH, IParametroHotelRepository
{
    public ParametroHotelRepository(ISession session) : base(session) { }

    public async Task<ParametroHotel?> GetByIdHotelAsync(long idHotel)
    {
        return await FindById<ParametroHotel>(idHotel);
    }
}

public class HospedeRepository : RepositoryNH, IHospedeRepository
{
    public HospedeRepository(ISession session) : base(session) { }
}

public class MovimentoHospedeRepository : RepositoryNH, IMovimentoHospedeRepository
{
    public MovimentoHospedeRepository(ISession session) : base(session) { }
}

public class ReservaReduzidaRepository : RepositoryNH, IReservaReduzidaRepository
{
    public ReservaReduzidaRepository(ISession session) : base(session) { }

    public async Task AddAsync(ReservaReduzida reservaReduzida)
    {
        await Save(reservaReduzida);
    }
}
