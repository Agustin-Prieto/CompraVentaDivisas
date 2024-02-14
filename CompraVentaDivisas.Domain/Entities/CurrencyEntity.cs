namespace CompraVentaDivisas.Domain.Entities;

public sealed class CurrencyEntity
{
    public CurrencyEntity(Guid currencyId, string name, string symbol)
    {
        CurrencyId = currencyId;
        Name = name;
        Symbol = symbol;
    }
    public Guid CurrencyId { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
}
