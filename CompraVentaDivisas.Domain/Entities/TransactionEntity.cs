using CompraVentaDivisas.Domain.Enums;

namespace CompraVentaDivisas.Domain.Entities;

public sealed class TransactionEntity
{
    public TransactionEntity(Guid id, decimal amount, decimal amountInPesos, DateTime date, TransactionType type, Guid clientId, Guid currencyId, Guid exchangeRateId)
    {
        Id = id;
        AmountOperated = amount;
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
