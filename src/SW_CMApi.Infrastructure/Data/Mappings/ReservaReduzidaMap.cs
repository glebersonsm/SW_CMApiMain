using FluentNHibernate.Mapping;
using SW_CMApi.Domain.Entities;

namespace SW_CMApi.Infrastructure.Data.Mappings;

public class ReservaReduzidaMap : ClassMap<ReservaReduzida>
{
    public ReservaReduzidaMap()
    {
        Table("RESERVAREDUZ");
        
        Id(x => x.IdReserva, "idreservasfront").GeneratedBy.Assigned(); 
        // Logic: ReservaReduzida shares ID with Reserva (OneToOne logic often) or just manual insert

        Map(x => x.IdHotel, "idhotel");
        Map(x => x.DataCheckinPrevisto, "datachegada");
        Map(x => x.DataCheckoutPrevisto, "datapartida");
        Map(x => x.StatusReserva, "statusreserva");
        Map(x => x.IdFornecedorCliente, "idforcli");
        Map(x => x.CodigoContrato, "codcontrato");
        Map(x => x.TipoUh, "idtipouh");
        Map(x => x.TrgDataInclusao, "trgdtinclusao");
        Map(x => x.UsuarioInclusao, "trguserinclusao").ReadOnly(); 
    }
}
