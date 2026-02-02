using FluentNHibernate.Mapping;
using SW_CMApi.Domain.Entities;

namespace SW_CMApi.Infrastructure.Data.Mappings;

public class HospedeMap : ClassMap<Hospede>
{
    public HospedeMap()
    {
        Table("hospede");
        Id(x => x.IdHospede, "idhospede").GeneratedBy.Native();

        Map(x => x.IdCidade, "idcidades");
        Map(x => x.IdIdioma, "ididioma");
        Map(x => x.IdFaixaEtaria, "idfaixaetaria");
        Map(x => x.SobreNome, "sobrenome");
        Map(x => x.Nome, "nome"); // Column name matches
        Map(x => x.RecebeMalaDireta, "recebemaladireta");
        Map(x => x.DataNascimento, "datanascimento");
        Map(x => x.TipoEtario, "tipoetario");
        Map(x => x.Fumante, "fumante");
        Map(x => x.UsuarioInclusao, "trguserinclusao");
        Map(x => x.UsuarioAlteracao, "trguseralteracao");
        Map(x => x.BloqueiaLancamentoManual, "FLGBLOQUEADOLANCMANUAL");
        Map(x => x.IdTipoHospede, "idtipohospede");
    }
}
