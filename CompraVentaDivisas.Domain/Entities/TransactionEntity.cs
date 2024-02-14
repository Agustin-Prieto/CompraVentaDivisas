using CompraVentaDivisas.Domain.Enums;

namespace CompraVentaDivisas.Domain.Entities;

public sealed class TransactionEntity
{
    public TransactionEntity(
        Guid id, 
        decimal amountOperated, 
        decimal amountInPesos, 
        DateTime date, 
        TransactionType type
        )
    {
        Id = id;
        AmountOperated = amountOperated;
        AmountInPesos = amountInPesos;
        Date = date;
        Type = type;
    }
    public Guid Id { get; set; }
    public decimal AmountOperated { get; set; }
    public decimal AmountInPesos { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public ClientEntity Client { get; set; }
    public CurrencyEntity Currency { get; set; }
    public ExchangeRateEntity ExchangeRate { get; set; }
}
