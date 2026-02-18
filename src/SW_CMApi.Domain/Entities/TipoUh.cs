using System;

namespace SW_CMApi.Domain.Entities;

public class TipoUh
{
    // Chave composta IdHotel, IdTipoUh
    public virtual long IdHotel { get; set; }
    public virtual long IdTipoUh { get; set; }
    public virtual string? CodigoReduzido { get; set; }
    public virtual string? Descricao { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        
        var other = (TipoUh)obj;
        return IdHotel == other.IdHotel && IdTipoUh == other.IdTipoUh;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IdHotel, IdTipoUh);
    }
}
