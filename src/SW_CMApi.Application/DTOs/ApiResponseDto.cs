namespace SW_CMApi.Application.DTOs;

public record ApiResponseDto<T>(
    int Status,
    bool Success,
    T Data,
    string Message
);
