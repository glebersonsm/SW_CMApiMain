using SW_CMApi.Domain.Enums;
using System;

namespace SW_CMApi.Domain.Entities;

public class Reserva
{
    public virtual long IdReserva { get; set; }
    public virtual long NumeroReserva { get; set; }
    public virtual string? Observacao { get; set; }
    public virtual long IdHotel { get; set; }
    public virtual DateTime? DataCheckinPrevisto { get; set; }
    public virtual DateTime? HoraCheckin { get; set; }
    public virtual DateTime? DataCheckinReal { get; set; }
    public virtual DateTime? DataCheckoutPrevisto { get; set; }
    public virtual DateTime? HoraCheckout { get; set; }
    public virtual DateTime? DataCheckoutReal { get; set; }
    public virtual string? ObservaoSensivel { get; set; }
    public virtual string? ObservacaoCmNet { get; set; }
    public virtual long? StatusReserva { get; set; }
    public virtual long? Usuario { get; set; }
    public virtual decimal ValorDiaria { get; set; } = 0;
    public virtual long? IdTarifa { get; set; }
    public virtual string? CodigoPensao { get; set; }
    public virtual string? CodigoSegmento { get; set; }
    public virtual long? IdOrigem { get; set; }
    public virtual long? IdCliente { get; set; }
    
    public virtual long? IdTipoUhEstadia { get; set; }
    public virtual long? IdTipoUhTarifa { get; set; }
    
    // Navigation Properties
    public virtual TipoUh? TipoUh { get; set; }
    public virtual TipoUh? TipoUhTarifa { get; set; }

    public virtual string? CodigoUh { get; set; }
    public virtual long? IdDocumento { get; set; }
    public virtual long? IdMeioComunicacao { get; set; }
    public virtual long? IdVeiculo { get; set; }
    public virtual long? IdMotivo { get; set; }
    public virtual string? Reservante { get; set; }
    public virtual string? TelefoneReservante { get; set; }
    public virtual long? QuantidadeAdulto { get; set; }
    public virtual long? QuantidadeCrianca1 { get; set; }
    public virtual long? QuantidadeCrianca2 { get; set; }
    public virtual decimal? PercentualDescontoDiaria { get; set; }
    public virtual string GaranteNoShow { get; set; } = "N";
    public virtual DateTime? DataReserva { get; set; }
    public virtual DateTime? HoraReserva { get; set; }
    public virtual string? Ajuste { get; set; }
    public virtual string? PoolLista { get; set; }
    public virtual decimal? ValorUpSailing { get; set; }
    public virtual string? AutoCheckOut { get; set; }
    public virtual string? Walkin { get; set; }
    public virtual string? Documento { get; set; }
    public virtual string? Compartilhada { get; set; }
    public virtual decimal? ValorDiariaPadrao { get; set; }
    public virtual string? DiariaFixa { get; set; }
    public virtual long? LocalizadorReserva { get; set; }
    public virtual decimal? ValorPensao { get; set; }
    public virtual decimal? ValorCafe { get; set; }
    public virtual string? EmailReservante { get; set; }
    public virtual string? UsuarioInclusao { get; set; }
    public virtual string? Mensalista { get; set; }
    public virtual long? NumeroReservaPrincipal { get; set; }
    public virtual decimal? ValorDiariaSemImposto { get; set; }
    public virtual string? NumeroCelular { get; set; }
    public virtual string? MantemAlteracaoManual { get; set; }
    public virtual DateTime? DataCancelamento { get; set; }
    public virtual string? ObservacaoCancelamento { get; set; }
    public virtual DateTime? DataConfirmacao { get; set; }
    public virtual DateTime? DataNoShow { get; set; }
    
    // Auditoria
    public virtual DateTime? DataAlteracao { get; set; }
    public virtual DateTime? DataInclusao { get; set; }
    public virtual DateTime? TriggerDataAlteracao { get; set; }
    public virtual string? UsuarioAlteracao { get; set; }
    public virtual DateTime? UltimaAlteracao { get; set; }
    
    public virtual string? CodigoReferencia { get; set; }
    public virtual long? IdCodigoMoede { get; set; }
    public virtual long? IdUsuarioAlteracao { get; set; }
    public virtual string TipoDeUso { get; set; } = "UP";

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
