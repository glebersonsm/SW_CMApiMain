using SW_CMApi.Application.DTOs;
using SW_CMApi.Application.Interfaces;
using SW_CMApi.Domain.Entities;
using SW_CMApi.Domain.Enums;
using SW_CMApi.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW_CMApi.Application.Services;

public class ReservaService : IReservaService
{
    private readonly IReservaRepository _reservaRepository;

    public ReservaService(IReservaRepository reservaRepository)
    {
        _reservaRepository = reservaRepository;
    }

    public async Task<ReservaResponseDataDto> SalvarReservaAsync(ReservaRequestDto reservaDto)
    {
        ValidarReserva(reservaDto);

        // TODO: Buscar ParametroHotel
        // TODO: Extrair HospedePrincipal
        // TODO: BuscarOuCriarReserva
        // TODO: VincularClienteReservante
        // TODO: Salvar no Repository
        // TODO: CriarReservaReduzida
        // TODO: CriarOrcamentoReserva
        // TODO: CriarHospedesReserva
        
        throw new NotImplementedException("Funcionalidade de Salvar Reserva em construção. Depende de múltiplos serviços externos.");
    }

    public async Task<string> CancelarReservaAsync(ReservaCancelarRequestDto reservaCancelar)
    {
        if (reservaCancelar.IdReseva == 0)
        {
            throw new ArgumentException("Não informado o número da reserva a cancelar");
        }

        var reserva = await _reservaRepository.GetByNumeroReservaAsync(reservaCancelar.IdReseva);
        if (reserva == null)
        {
            throw new KeyNotFoundException($"Reserva com número {reservaCancelar.IdReseva} não encontrada!");
        }

        if (reserva.StatusReserva == (long)StatusReserva.CANCELADA)
        {
            return $"A Reserva {reservaCancelar.IdReseva} já esta cancelada";
        }

        if (reserva.StatusReserva == (long)StatusReserva.CHECKIN)
        {
            throw new InvalidOperationException("Reserva em checkin não pode ser cancelada");
        }

        if (reserva.StatusReserva == (long)StatusReserva.CHECKOUT)
        {
            throw new InvalidOperationException("Reserva em checkout não pode ser cancelada");
        }

        if (reserva.StatusReserva == (long)StatusReserva.PENDENTE)
        {
            throw new InvalidOperationException("Reserva pendente não pode ser cancelada");
        }

        if (reserva.StatusReserva == (long)StatusReserva.NOSHOW) // NoShow tem valor 3L também, cuidado no enum
        {
            // Se o valor for o mesmo de CHECKOUT, essa validação é redundante mas ok
            throw new InvalidOperationException("Reserva NoShow não pode ser cancelada");
        }

        long idMotivo = 0;
        if (!long.TryParse(reservaCancelar.MotivoCancelamento, out idMotivo))
        {
             // Logica se não for numérico
        }

        reserva.Cancelar(idMotivo, reservaCancelar.ObservaoCancelamento);
        
        await _reservaRepository.UpdateAsync(reserva);
        await _reservaRepository.SaveChangesAsync();

        return "Reserva cancelada com sucesso!";
    }

    private void ValidarReserva(ReservaRequestDto reservaDto)
    {
        var hoje = DateTime.Today;

        if (reservaDto.CheckIn.Date < hoje)
        {
            throw new ArgumentException("Data de Checkin deve maior ou igual a data atual");
        }

        if (reservaDto.CheckIn.Date > reservaDto.CheckOut.Date)
        {
             throw new ArgumentException("Data de Checkin não pode ser maior que a data checkout");
        }

        if (reservaDto.CheckOut.Date < hoje)
        {
             throw new ArgumentException("Data de Checkout deve maior ou igual a data atual");
        }
        
        // Outras validações...
    }
}
