using CompraVentaDivisas.Domain.Enums;

namespace CompraVentaDivisas.Domain.Entities;

public sealed class TransactionEntity
{
    public TransactionEntity(Guid id, decimal amount, DateTime date, TransactionType type, Guid clientId, Guid currencyId, Guid exchangeRateId)
    {
        Id = id;
        Amount = amount;
        Date = date;
        Type = type;
        ClientId = clientId;
        CurrencyId = currencyId;
        ExchangeRateId = exchangeRateId;
    }
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public Guid ClientId { get; set; }
    public Guid CurrencyId { get; set; }
    public Guid ExchangeRateId { get; set; }
}
