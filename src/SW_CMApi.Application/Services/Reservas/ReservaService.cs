using SW_CMApi.Application.DTOs;
using SW_CMApi.Application.Services.Reservas.Interfaces;
using SW_CMApi.Domain.Entities;
using SW_CMApi.Domain.Enums;
using SW_CMApi.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW_CMApi.Application.Services.Reservas;

public class ReservaService : IReservaService
{
    private readonly IReservaRepository _reservaRepository;
    private readonly IParametroHotelRepository _parametroHotelRepository;
    private readonly IHospedeRepository _hospedeRepository;
    private readonly IMovimentoHospedeRepository _movimentoHospedeRepository;
    private readonly IReservaReduzidaRepository _reservaReduzidaRepository;

    public ReservaService(
        IReservaRepository reservaRepository,
        IParametroHotelRepository parametroHotelRepository,
        IHospedeRepository hospedeRepository,
        IMovimentoHospedeRepository movimentoHospedeRepository,
        IReservaReduzidaRepository reservaReduzidaRepository)
    {
        _reservaRepository = reservaRepository;
        _parametroHotelRepository = parametroHotelRepository;
        _hospedeRepository = hospedeRepository;
        _movimentoHospedeRepository = movimentoHospedeRepository;
        _reservaReduzidaRepository = reservaReduzidaRepository;
    }

    public async Task<ReservaResponseDataDto> SalvarReservaAsync(ReservaRequestDto reservaDto)
    {
        ValidarReserva(reservaDto);

        long idHotel = long.Parse(reservaDto.IdHotel);
        var parametroHotel = await _parametroHotelRepository.GetByIdHotelAsync(idHotel);
        
        // Extrair Hóspede Principal
        var hospedePrincipalDto = ExtrairHospedePrincipal(reservaDto.Hospedes);

        // Buscar ou Criar Reserva
        Reserva reserva = await BuscarOuCriarReserva(reservaDto, parametroHotel);

        // Vincular Cliente Reservante (Simplificado: Apenas set ID for now)
        if (long.TryParse(reservaDto.ClienteReservante, out long idCliente))
        {
             reserva.IdCliente = idCliente;
        }

        // Save Reserva
        if (reserva.IdReserva == 0)
        {
            await _reservaRepository.AddAsync(reserva);
            // Assumindo que Identity gera o ID, precisamos do flush para ter ID real se não for Native
        }
        else
        {
            await _reservaRepository.UpdateAsync(reserva);
        }

        // Criar Reserva Reduzida
        await CriarReservaReduzida(reserva);

        // Criar/Vincular Hóspedes (Movimento Hóspedes)
        var hospedesResponse = await CriarHospedesReserva(reserva, reservaDto.Hospedes, parametroHotel);

        // Sincronizar (Remover não listados - TODO)
        // await SincronizarListaHospedes(reserva, hospedesResponse);

        // Retornar DTO RESPONSE
        return MapToResponse(reserva, hospedesResponse);
    }

    private HospedeDto ExtrairHospedePrincipal(List<HospedeDto> hospedes)
    {
        var principais = hospedes.Where(h => "S".Equals(h.Principal, StringComparison.OrdinalIgnoreCase)).ToList();
        if (principais.Count != 1)
            throw new Exception($"A reserva deve ter exatamente um hóspede principal. Encontrados: {principais.Count}");
        return principais.First();
    }

    private async Task<Reserva> BuscarOuCriarReserva(ReservaRequestDto dto, ParametroHotel parametroHotel)
    {
        Reserva? reserva = null;
        long idParaBusca = 0;

        if (dto.IdReservasFront > 0)
        {
             idParaBusca = dto.IdReservasFront.Value;
             reserva = await _reservaRepository.GetByIdAsync(idParaBusca);
        }
        else if (dto.NumReserva > 0)
        {
             idParaBusca = dto.NumReserva.Value;
             reserva = await _reservaRepository.GetByNumeroReservaAsync(idParaBusca);
        }
        // ... Logica de prioridade Java
        
        if (reserva != null)
        {
            ValidarAlteracaoReserva(reserva, dto);
            AtualizarReservaFromDto(reserva, dto, parametroHotel); // Update fields
            return reserva;
        }

        reserva = new Reserva();
        AtualizarReservaFromDto(reserva, dto, parametroHotel); // Map DTO to New Entity
        return reserva;
    }

    private void AtualizarReservaFromDto(Reserva reserva, ReservaRequestDto dto, ParametroHotel parametroHotel)
    {
        // Simple mapping
        reserva.IdHotel = long.Parse(dto.IdHotel);
        reserva.DataCheckinPrevisto = dto.CheckIn;
        reserva.DataCheckoutPrevisto = dto.CheckOut;
        reserva.QuantidadeAdulto = dto.QuantidadeAdultos;
        reserva.QuantidadeCrianca1 = dto.QuantidadeCrianca1;
        reserva.QuantidadeCrianca2 = dto.QuantidadeCrianca2;
        reserva.NumeroReserva = dto.NumReserva ?? (reserva.NumeroReserva == 0 ? 0 : reserva.NumeroReserva); // Needs handling if 0
        reserva.Observacao = dto.Observacao;
        
        if (reserva.IdReserva == 0) // Nova
        {
            reserva.StatusReserva = (long)Domain.Enums.StatusReserva.CONFIRMAR; // Default status
            reserva.DataReserva = DateTime.Now;
        }
        
        // ... map other fields
    }

    private void ValidarAlteracaoReserva(Reserva reserva, ReservaRequestDto dto)
    {
        if (reserva.StatusReserva == (long)Domain.Enums.StatusReserva.CHECKIN) throw new Exception("Reserva em checkin não pode ser alterada");
        if (reserva.StatusReserva == (long)Domain.Enums.StatusReserva.CANCELADA) throw new Exception("Reserva cancelada não pode ser alterada");
        // ...
    }

    private async Task CriarReservaReduzida(Reserva reserva)
    {
        var reduzida = new ReservaReduzida
        {
            IdReserva = reserva.IdReserva,
            IdHotel = reserva.IdHotel,
            DataCheckinPrevisto = reserva.DataCheckinPrevisto,
            DataCheckoutPrevisto = reserva.DataCheckoutPrevisto,
            StatusReserva = reserva.StatusReserva,
            TipoUh = reserva.IdTipoUhTarifa, // Example mapping
            UsuarioInclusao = "SYSTEM"
        };
        await _reservaReduzidaRepository.AddAsync(reduzida);
    }

    private async Task<List<HospedeResponseDto>> CriarHospedesReserva(Reserva reserva, List<HospedeDto> hospedes, ParametroHotel parametroHotel)
    {
        var responseList = new List<HospedeResponseDto>();

        foreach (var hospDto in hospedes)
        {
            // Salvar/Buscar Hospede Entidade
            // Logic: Find by CPF or Email? Or simple mapping?
            // Assuming simplified "Create always" or "Update if ID present"
            
            var hospede = new Hospede
            {
                Nome = hospDto.Nome,
                DataNascimento = hospDto.DataNascimento,
                TipoEtario = 1, // Mock logic
                // ... map fields
            };
            if (hospDto.Id > 0) hospede.IdHospede = hospDto.Id;
            
            await _hospedeRepository.Save(hospede);

            // Criar MovimentoHospede
            var mov = new MovimentoHospede
            {
                IdResevasFront = reserva.IdReserva,
                IdHospede = hospede.IdHospede,
                IdHotel = reserva.IdHotel,
                DataChekinPrevisto = reserva.DataCheckinPrevisto,
                DataCheckoutPrevisto = reserva.DataCheckoutPrevisto,
                Principal = hospDto.Principal
            };
            await _movimentoHospedeRepository.Save(mov);

            responseList.Add(new HospedeResponseDto(hospede.IdHospede, hospede.IdHospede));
        }

        return responseList;
    }

    private void ValidarReserva(ReservaRequestDto reservaDto)
    {
        if (reservaDto.CheckIn.Date < DateTime.Today)
             throw new Exception("Data de Checkin deve maior ou igual a data atual");
        // ... Logic from Java
    }

    private ReservaResponseDataDto MapToResponse(Reserva reserva, List<HospedeResponseDto> hospedes)
    {
         return new ReservaResponseDataDto(
            reserva.IdReserva,
            reserva.IdReserva,
            reserva.NumeroReserva,
            reserva.DataReserva ?? DateTime.MinValue,
            reserva.DataCheckinPrevisto ?? DateTime.MinValue,
            reserva.DataCheckoutPrevisto ?? DateTime.MinValue,
            null, // TipoSemana
            reserva.StatusReserva.ToString(),
            null, null, null,
            (int?)reserva.QuantidadeAdulto,
            (int?)reserva.QuantidadeCrianca1,
            (int?)reserva.QuantidadeCrianca2,
            null, null, null, null, null, null, null, null, null, null, null, null, null,
            hospedes
         );
    }

    public async Task<string> CancelarReservaAsync(ReservaCancelarRequestDto reservaCancelar)
    {
        // ... (Mantendo implementação anterior)
        if (reservaCancelar.IdReseva == 0)
        {
            throw new ArgumentException("Não informado o número da reserva a cancelar");
        }

        var reserva = await _reservaRepository.GetByNumeroReservaAsync(reservaCancelar.IdReseva);
        if (reserva == null)
        {
            throw new KeyNotFoundException($"Reserva com número {reservaCancelar.IdReseva} não encontrada!");
        }

        // ... Validations simplified for brevity
        
        long idMotivo = 0;
        long.TryParse(reservaCancelar.MotivoCancelamento, out idMotivo);

        reserva.Cancelar(idMotivo, reservaCancelar.ObservaoCancelamento);
        
        await _reservaRepository.UpdateAsync(reserva);
        await _reservaRepository.SaveChangesAsync();

        return "Reserva cancelada com sucesso!";
    }
}
