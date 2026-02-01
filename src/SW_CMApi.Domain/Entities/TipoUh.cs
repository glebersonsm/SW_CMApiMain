using System;

namespace SW_CMApi.Domain.Entities;

public class TipoUh
{
    // Chave composta IdHotel, IdTipoUh
    public long IdHotel { get; set; }
    public long IdTipoUh { get; set; }
    public string? CodigoReduzido { get; set; }
}
