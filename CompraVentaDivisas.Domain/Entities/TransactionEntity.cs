using CompraVentaDivisas.Domain.Enums;
using FluentResults;

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

    public static Result<bool> IsValidPurchase(Guid clientId, Guid currencyId, Guid exchangeRateId, decimal amount)
    {
        var result = new Result<bool>();
        return result;
    }

    public static Result<bool> IsValidSale(DateTime date)
    {
        var result = new Result<bool>();
        return result;
    }

    public static Result<TransactionEntity> RegisterPurchase(Guid clientId, Guid currencyId, Guid exchangeRateId, decimal amount)
    {
        var result = new Result<TransactionEntity>();
        return result;
    }

    public static Result<TransactionEntity> RegisterSale(Guid clientId, Guid currencyId, Guid exchangeRateId, decimal amount)
    {
        var result = new Result<TransactionEntity>();
        return result;
    }
}
