using CompraVentaDivisas.Domain.Entities;

namespace CompraVentaDivisas.Application.Abastractions;

public interface ITransactionRepository
{
    Task<IEnumerable<TransactionEntity>> GetAllTransactionsAsync();
    Task<TransactionEntity> GetTransactionByIdAsync(int id);
    Task InsertTransactionAsync(TransactionEntity currency);
    Task UpdateTransactionAsync(TransactionEntity currency);
    Task DeleteTransactionAsync(int id);
}
