namespace CompraVentaDivisas.Domain.Entities;

public sealed class CurrencyEntity
{
    public CurrencyEntity(Guid id, string currencyName, string currencyCode)
    {
        Id = id;
        Name = currencyName;
        Symbol = currencyCode;
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
}
