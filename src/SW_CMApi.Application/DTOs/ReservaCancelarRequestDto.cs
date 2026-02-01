namespace SW_CMApi.Application.DTOs;

public record ReservaCancelarRequestDto(
    long IdReseva,
    string MotivoCancelamento,
    string ObservaoCancelamento
);
