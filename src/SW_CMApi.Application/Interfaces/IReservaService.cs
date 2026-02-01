using SW_CMApi.Application.DTOs;
using System.Threading.Tasks;

namespace SW_CMApi.Application.Interfaces;

public interface IReservaService
{
    Task<ReservaResponseDataDto> SalvarReservaAsync(ReservaRequestDto reservaDto);
    Task<string> CancelarReservaAsync(ReservaCancelarRequestDto reservaCancelar);
}
