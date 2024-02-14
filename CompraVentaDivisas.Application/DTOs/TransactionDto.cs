using CompraVentaDivisas.Domain.Enums;

namespace CompraVentaDivisas.Application.DTOs;

public sealed class TransactionDto
{
    public TransactionDto(
        Guid id, 
        decimal amountOperated, 
        decimal amountInPesos, 
        DateTime date, 
        TransactionType type,
        Guid currencyId,
        Guid exchangeRateId,
        Guid clientId
        )
    {
        Id = id;
        AmountOperated = amountOperated;
        AmountInPesos = amountInPesos;
        Date = date;
        Type = type;
        ClientId = clientId;
        CurrencyId = currencyId;
        ExchangeRateId = exchangeRateId;
    }
    public Guid Id { get; set; }
    public decimal AmountOperated { get; set; }
    public decimal AmountInPesos { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public Guid ClientId { get; set; }
    public Guid CurrencyId { get; set; }
    public Guid ExchangeRateId { get; set; }
}
