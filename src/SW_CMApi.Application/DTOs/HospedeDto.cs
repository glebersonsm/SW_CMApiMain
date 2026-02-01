using System;

namespace SW_CMApi.Application.DTOs;

public record HospedeDto(
    long Id,
    long? IdHospede,
    string TipoHospede,
    long? ClienteId,
    string Principal,
    string Nome,
    string Cpf,
    DateTime DataNascimento,
    string Email,
    string Telefone,
    string Sexo,
    string CodigoIbge,
    string Logradouro,
    string Numero,
    string Bairro,
    string Complemento,
    string Cep,
    DateTime DataCheckin,
    DateTime DataCheckOut
);
