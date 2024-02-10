using CompraVentaDivisas.Domain.Enums;

namespace CompraVentaDivisas.Domain.Entities;

public sealed class ExchangeRateEntity
{
    public ExchangeRateEntity(Guid id, decimal buyValue, decimal sellValue, DateTime date, ExchangeRateType type, Guid currencyId)
    {
        Id = id;
        BuyValue = buyValue;
        SellValue = sellValue;
        Date = date;
        Type = type;
        CurrencyId = currencyId;
    }

    public Guid Id { get; set; }
    public decimal BuyValue { get; set; }
    public decimal SellValue { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public ExchangeRateType Type { get; set; }
    public Guid CurrencyId { get; set; }
    //public CurrencyEntity Currency { get; set; }
}
