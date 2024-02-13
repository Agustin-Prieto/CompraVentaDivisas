using FluentResults;

namespace CompraVentaDivisas.Domain.Entities;

public sealed class ClientEntity
{
    public ClientEntity(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }

    public static Result<IEnumerable<TransactionEntity>> GetTransactions()
    {
        return Result.Ok<IEnumerable<TransactionEntity>>(new List<TransactionEntity>());
    }

    public static Result<decimal> MonthlyPurchaseAmount()
    {
        return Result.Ok<decimal>(0);
    }
}
