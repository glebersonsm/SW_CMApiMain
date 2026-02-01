using FluentNHibernate.Mapping;
using SW_CMApi.Domain.Entities;
using SW_CMApi.Domain.Enums;

namespace SW_CMApi.Infrastructure.Data.Mappings;

public class ReservaMap : ClassMap<Reserva>
{
    public ReservaMap()
    {
        Table("reservasfront");
        
        Id(x => x.IdReserva, "idreservasfront").GeneratedBy.Native();

        Map(x => x.NumeroReserva, "numreserva").Not.Nullable();
        Map(x => x.Observacao, "observacoes");
        Map(x => x.IdHotel, "idhotel");
        Map(x => x.DataCheckinPrevisto, "datachegprevista");
        Map(x => x.HoraCheckin, "horachegprevista");
        Map(x => x.DataCheckinReal, "datachegadareal");
        Map(x => x.DataCheckoutPrevisto, "datapartprevista");
        Map(x => x.HoraCheckout, "horapartprevista");
        Map(x => x.DataCheckoutReal, "datapartidareal");
        Map(x => x.ObservaoSensivel, "obssensiveis");
        Map(x => x.ObservacaoCmNet, "obscmnet");
        
        // Mapeando Enum como long
        Map(x => x.StatusReserva, "statusreserva").CustomType<long>(); 
        
        Map(x => x.Usuario, "usuario");
        Map(x => x.ValorDiaria, "vlrdiaria");
        Map(x => x.IdTarifa, "idtarifa");
        Map(x => x.CodigoPensao, "codpensao");
        Map(x => x.CodigoSegmento, "codsegmento");
        Map(x => x.IdOrigem, "idorigem");
        Map(x => x.IdCliente, "clientereservante");
        
        Map(x => x.CodigoUh, "coduh");
        // ... outros campos ...
        Map(x => x.QuantidadeAdulto, "adultos");
        Map(x => x.QuantidadeCrianca1, "criancas1");
        Map(x => x.QuantidadeCrianca2, "criancas2");
        
        Map(x => x.DataCancelamento, "datacancelamento");
        Map(x => x.ObservacaoCancelamento, "obscancelamento");
        Map(x => x.IdMotivo, "idmotivo");
        Map(x => x.LocalizadorReserva, "locreserva");

        // Relacionamentos (ignorando composite keys complexas por enquanto para simplificar o build inicial, 
        // mas no projeto real precisaria CompositeId ou referencias manuais)
    }
}
