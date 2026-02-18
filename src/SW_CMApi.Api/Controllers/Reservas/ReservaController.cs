using Microsoft.AspNetCore.Mvc;
using SW_CMApi.Application.DTOs;
using SW_CMApi.Application.Services.Reservas.Interfaces;
using System.Threading.Tasks;

namespace SW_CM_Reservas.Api.Controllers.Reservas;

[ApiController]
[Route("Reserva/v1")]
public class ReservaController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservaController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    [HttpPost("efetuarReserva")]
    public async Task<IActionResult> CriarReserva([FromBody] ReservaRequestDto reservaDto)
    {
        try
        {
            var dadosDaReserva = await _reservaService.SalvarReservaAsync(reservaDto);
            var respostaApi = new ApiResponseDto<ReservaResponseDataDto>(201, true, dadosDaReserva, "Reserva criada com sucesso.");
            return Created("", respostaApi);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponseDto<object>(400, false, null, ex.Message));
        }
    }

    [HttpPost("cancelarReserva")]
    public async Task<IActionResult> CancelarReserva([FromBody] ReservaCancelarRequestDto reservaCancelar)
    {
        try
        {
            var mensagem = await _reservaService.CancelarReservaAsync(reservaCancelar);
            var respostaApi = new ApiResponseDto<object>(200, true, null, mensagem);
            return Ok(respostaApi);
        }
        catch (Exception ex)
        {
             return BadRequest(new ApiResponseDto<object>(400, false, null, ex.Message));
        }
    }
}
