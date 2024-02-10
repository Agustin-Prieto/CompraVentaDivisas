namespace CompraVentaDivisas.Domain.Entities;

public sealed class CurrencyEntity
{
    public CurrencyEntity(Guid id, string currencyName, string currencyCode)
    {
        Id = id;
        CurrencyName = currencyName;
        CurrencyCode = currencyCode;
    }
    public Guid Id { get; set; }
    public string CurrencyName { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
}
