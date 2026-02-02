using FluentNHibernate.Mapping;
using SW_CMApi.Domain.Entities;

namespace SW_CMApi.Infrastructure.Data.Mappings;

public class ParametroHotelMap : ClassMap<ParametroHotel>
{
    public ParametroHotelMap()
    {
        Table("paramhotel");
        Id(x => x.IdHotel, "idhotel").GeneratedBy.Assigned(); // PK is assigned (IdHotel itself)

        Map(x => x.HoraCheckIn, "horacheckin");
        Map(x => x.HoraChekOut, "horacheckout");
        Map(x => x.IdadeMaximaCrianca1, "idademaxcri1");
        Map(x => x.IdadeMaximaCrianca2, "idademaxcri2");
    }
}
