using FluentNHibernate.Mapping;
using SW_CMApi.Domain.Entities;

namespace SW_CMApi.Infrastructure.Data.Mappings;

public class MovimentoHospedeMap : ClassMap<MovimentoHospede>
{
    public MovimentoHospedeMap()
    {
        Table("movimentohospedes");
        
        // Composite ID mapping
        CompositeId()
            .KeyProperty(x => x.IdResevasFront, "idreservasfront")
            .KeyProperty(x => x.IdHospede, "idhospede");

        Map(x => x.IdHotel, "idhotel");
        Map(x => x.DataChekinPrevisto, "datachegprevista");
        Map(x => x.DataCheckinReal, "datachegreal");
        Map(x => x.DataCheckoutPrevisto, "datapartprevista");
        Map(x => x.DataCheckoutReal, "datapartreal");
        Map(x => x.IdTipoHospede, "idtipohospede");
        Map(x => x.Incognito, "incognito");
        Map(x => x.HoraCheckinPrevista, "horachegprevista");
        Map(x => x.HoraCheckoutPrevista, "horapartprevista");
        Map(x => x.UsoDaCasa, "usodacasa");
        Map(x => x.PercentualDiaria, "percdiaria");
        Map(x => x.DiariaConfidencial, "diariaconfidencia");
        Map(x => x.Cofre, "cofre");
        Map(x => x.Principal, "Principal"); // Case sensitive in DB? Usually not, but following Entity
        Map(x => x.MenorIdade, "menoridade");
        Map(x => x.IdResponsavel, "idresponsavel");
        Map(x => x.UsuarioInclusao, "trguserinclusao");
        Map(x => x.DataInclusao, "trgdtinclusao");
    }
}
