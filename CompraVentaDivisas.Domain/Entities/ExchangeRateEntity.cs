using CompraVentaDivisas.Domain.Enums;

namespace CompraVentaDivisas.Domain.Entities;

public sealed class ExchangeRateEntity
{
    public ExchangeRateEntity(Guid exchangeRateId, decimal buyValue, decimal sellValue, DateTime date, ExchangeRateType type)
    {
        ExchangeRateId = exchangeRateId;
        BuyValue = buyValue;
        SellValue = sellValue;
        Date = date;
        Type = type;
    }

    public Guid ExchangeRateId { get; set; }
    public decimal BuyValue { get; set; }
    public decimal SellValue { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public ExchangeRateType Type { get; set; }
    public CurrencyEntity Currency { get; set; }
}
