using System;

namespace SW_CMApi.Domain.Entities;

public class ParametroHotel
{
    public virtual long IdHotel { get; set; }
    public virtual DateTime? HoraCheckIn { get; set; }
    public virtual DateTime? HoraChekOut { get; set; }
    public virtual long? IdadeMaximaCrianca1 { get; set; }
    public virtual long? IdadeMaximaCrianca2 { get; set; }
}
