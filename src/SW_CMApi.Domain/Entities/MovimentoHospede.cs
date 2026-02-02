using System;

namespace SW_CMApi.Domain.Entities;

public class MovimentoHospede
{
    // Composite Key Properties directly in class or nested? 
    // NHibernate handles composite keys well. Let's use properties here and map as CompositeId.
    public long IdResevasFront { get; set; }
    public long IdHospede { get; set; }

    public long? IdHotel { get; set; }
    public DateTime? DataChekinPrevisto { get; set; }
    public DateTime? DataCheckinReal { get; set; }
    public DateTime? DataCheckoutPrevisto { get; set; }
    public DateTime? DataCheckoutReal { get; set; }
    public long? IdTipoHospede { get; set; }
    public string Incognito { get; set; } = "N";
    public DateTime? HoraCheckinPrevista { get; set; }
    public DateTime? HoraCheckoutPrevista { get; set; }
    public string UsoDaCasa { get; set; } = "N";
    public decimal PercentualDiaria { get; set; } = 0;
    public string DiariaConfidencial { get; set; } = "N";
    public string Cofre { get; set; } = "N";
    public string Principal { get; set; } = "N";
    public string MenorIdade { get; set; } = "N";
    public long? IdResponsavel { get; set; }
    public string UsuarioInclusao { get; set; }
    public DateTime? DataInclusao { get; set; }

    // Override Equals and GetHashCode is important for Composite Keys in NHibernate/EfCore
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        
        var other = (MovimentoHospede)obj;
        return IdResevasFront == other.IdResevasFront && IdHospede == other.IdHospede;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(IdResevasFront, IdHospede);
    }
}
