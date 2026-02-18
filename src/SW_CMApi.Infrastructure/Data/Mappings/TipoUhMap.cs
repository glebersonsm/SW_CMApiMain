using FluentNHibernate.Mapping;
using SW_CMApi.Domain.Entities;

namespace SW_CMApi.Infrastructure.Data.Mappings;

public class TipoUhMap : ClassMap<TipoUh>
{
    public TipoUhMap()
    {
        Table("tipouh");
        Schema("cm");

        CompositeId()
            .KeyProperty(x => x.IdHotel, "idhotel")
            .KeyProperty(x => x.IdTipoUh, "idtipouh");

        Map(x => x.CodigoReduzido, "codreduzido");
    }
}
