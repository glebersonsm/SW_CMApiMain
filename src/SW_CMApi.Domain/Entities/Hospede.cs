using System;

namespace SW_CMApi.Domain.Entities;

public class Hospede
{
    public long IdHospede { get; set; }
    public long? IdCidade { get; set; } // Simplified relation
    public long? IdIdioma { get; set; }
    public long? IdFaixaEtaria { get; set; } // Simplified relation
    public string SobreNome { get; set; }
    public string Nome { get; set; }
    public string RecebeMalaDireta { get; set; }
    public DateTime? DataNascimento { get; set; }
    public long? TipoEtario { get; set; }
    public string Fumante { get; set; } = "N";
    public string UsuarioInclusao { get; set; }
    public string UsuarioAlteracao { get; set; }
    public string BloqueiaLancamentoManual { get; set; } = "N";
    public long? IdTipoHospede { get; set; }
}
