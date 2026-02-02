using System;

namespace SW_CMApi.Domain.Entities;

public class ParametroHotel
{
    public long IdHotel { get; set; }
    public DateTime? HoraCheckIn { get; set; }
    public DateTime? HoraChekOut { get; set; }
    public long? IdadeMaximaCrianca1 { get; set; }
    public long? IdadeMaximaCrianca2 { get; set; }
}
