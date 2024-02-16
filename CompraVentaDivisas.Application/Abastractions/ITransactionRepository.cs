using CompraVentaDivisas.Application.DTOs;
using CompraVentaDivisas.Domain.Entities;

namespace CompraVentaDivisas.Application.Abastractions;

public interface ITransactionRepository
{
    Task<IEnumerable<TransactionEntity>> GetAllTransactionsAsync();
    Task<TransactionEntity> GetTransactionByIdAsync(Guid id);
    Task<IEnumerable<TransactionEntity>> GetTransactionByClientIdAsync(Guid clientId);
    Task InsertTransactionAsync(TransactionDto transaction);
    Task UpdateTransactionAsync(TransactionEntity transaction);
    Task DeleteTransactionAsync(Guid id);
}
