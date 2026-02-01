using SW_CMApi.Domain.Enums;
using System;

namespace SW_CMApi.Domain.Entities;

public class Reserva
{
    public long IdReserva { get; set; }
    public long NumeroReserva { get; set; }
    public string? Observacao { get; set; }
    public long IdHotel { get; set; }
    public DateTime? DataCheckinPrevisto { get; set; }
    public DateTime? HoraCheckin { get; set; }
    public DateTime? DataCheckinReal { get; set; }
    public DateTime? DataCheckoutPrevisto { get; set; }
    public DateTime? HoraCheckout { get; set; }
    public DateTime? DataCheckoutReal { get; set; }
    public string? ObservaoSensivel { get; set; }
    public string? ObservacaoCmNet { get; set; }
    public long? StatusReserva { get; set; }
    public long? Usuario { get; set; }
    public decimal ValorDiaria { get; set; } = 0;
    public long? IdTarifa { get; set; }
    public string? CodigoPensao { get; set; }
    public string? CodigoSegmento { get; set; }
    public long? IdOrigem { get; set; }
    public long? IdCliente { get; set; }
    
    public long? IdTipoUhEstadia { get; set; }
    public long? IdTipoUhTarifa { get; set; }
    
    // Navigation Properties
    public virtual TipoUh? TipoUh { get; set; }
    public virtual TipoUh? TipoUhTarifa { get; set; }

    public string? CodigoUh { get; set; }
    public long? IdDocumento { get; set; }
    public long? IdMeioComunicacao { get; set; }
    public long? IdVeiculo { get; set; }
    public long? IdMotivo { get; set; }
    public string? Reservante { get; set; }
    public string? TelefoneReservante { get; set; }
    public long? QuantidadeAdulto { get; set; }
    public long? QuantidadeCrianca1 { get; set; }
    public long? QuantidadeCrianca2 { get; set; }
    public decimal? PercentualDescontoDiaria { get; set; }
    public string GaranteNoShow { get; set; } = "N";
    public DateTime? DataReserva { get; set; }
    public DateTime? HoraReserva { get; set; }
    public string? Ajuste { get; set; }
    public string? PoolLista { get; set; }
    public decimal? ValorUpSailing { get; set; }
    public string? AutoCheckOut { get; set; }
    public string? Walkin { get; set; }
    public string? Documento { get; set; }
    public string? Compartilhada { get; set; }
    public decimal? ValorDiariaPadrao { get; set; }
    public string? DiariaFixa { get; set; }
    public long? LocalizadorReserva { get; set; }
    public decimal? ValorPensao { get; set; }
    public decimal? ValorCafe { get; set; }
    public string? EmailReservante { get; set; }
    public string? UsuarioInclusao { get; set; }
    public string? Mensalista { get; set; }
    public long? NumeroReservaPrincipal { get; set; }
    public decimal? ValorDiariaSemImposto { get; set; }
    public string? NumeroCelular { get; set; }
    public string? MantemAlteracaoManual { get; set; }
    public DateTime? DataCancelamento { get; set; }
    public string? ObservacaoCancelamento { get; set; }
    public DateTime? DataConfirmacao { get; set; }
    public DateTime? DataNoShow { get; set; }
    
    // Auditoria
    public DateTime? DataAlteracao { get; set; }
    public DateTime? DataInclusao { get; set; }
    public DateTime? TriggerDataAlteracao { get; set; }
    public string? UsuarioAlteracao { get; set; }
    public DateTime? UltimaAlteracao { get; set; }
    
    public string? CodigoReferencia { get; set; }
    public long? IdCodigoMoede { get; set; }
    public long? IdUsuarioAlteracao { get; set; }
    public string TipoDeUso { get; set; } = "UP";

    // Métodos de Dominio
    public void Cancelar(long idMotivo, string observacaoCancelamento) {
        this.LocalizadorReserva = null;
        this.StatusReserva = (long)Enums.StatusReserva.CANCELADA;
        this.DataCancelamento = DateTime.Now;
        this.IdMotivo = idMotivo;
        this.ObservacaoCancelamento = observacaoCancelamento;
    }

    public void Confirmar() {
        this.DataConfirmacao = null;
        this.StatusReserva = (long)Enums.StatusReserva.CONFIRMADA;
    }

    public void AtualizarQuantidadeHospedes(long qtdeAdulto, long qtdeCrianca1, long qtdeCrianca2) {
        this.QuantidadeAdulto = qtdeAdulto;
        this.QuantidadeCrianca1 = qtdeCrianca1;
        this.QuantidadeCrianca2 = qtdeCrianca2;
    }
}
