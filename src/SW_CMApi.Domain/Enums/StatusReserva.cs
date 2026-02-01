namespace SW_CMApi.Domain.Enums;

public enum StatusReserva : long
{
    CONFIRMAR = 0,
    CONFIRMADA = 1,
    CHECKIN = 2,
    CHECKOUT = 3,
    PENDENTE = 4,
    NOSHOW = 3, // Tem o mesmo valor que CHECKOUT no original
    CANCELADA = 6
}
