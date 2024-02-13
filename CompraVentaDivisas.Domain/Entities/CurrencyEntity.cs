namespace CompraVentaDivisas.Domain.Entities;

public sealed class CurrencyEntity
{
    public CurrencyEntity(Guid id, string name, string symbol)
    {
        Id = id;
        Name = name;
        Symbol = symbol;
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
}
