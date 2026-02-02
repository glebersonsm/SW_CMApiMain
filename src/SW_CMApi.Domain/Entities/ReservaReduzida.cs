using System;

namespace SW_CMApi.Domain.Entities;

public class ReservaReduzida
{
    public long IdReserva { get; set; }
    public long IdHotel { get; set; }
    public DateTime? DataCheckinPrevisto { get; set; }
    public DateTime? DataCheckoutPrevisto { get; set; }
    public long? StatusReserva { get; set; }
    public long? IdFornecedorCliente { get; set; }
    public long? CodigoContrato { get; set; }
    public long? TipoUh { get; set; }
    public DateTime? TrgDataInclusao { get; set; }
    public string? UsuarioInclusao { get; set; }
}
