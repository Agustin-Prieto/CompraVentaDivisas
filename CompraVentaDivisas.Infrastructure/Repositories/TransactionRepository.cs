﻿using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Domain.Entities;

namespace CompraVentaDivisas.Infrastructure.Repositories;

public sealed class TransactionRepository : ITransactionRepository
{
    private readonly ISqlDataAccess _sqlDataAccess;

    public TransactionRepository(ISqlDataAccess sqlDataAccess)
    {
        _sqlDataAccess = sqlDataAccess;
    }

    public Task<IEnumerable<TransactionEntity>> GetAllTransactionsAsync() =>
        _sqlDataAccess.LoadData<TransactionEntity, dynamic>("dbo.spTransaction_GetAll", new { });

    public async Task<TransactionEntity> GetTransactionByIdAsync(int id)
    {
        var result = await _sqlDataAccess.LoadData<TransactionEntity, dynamic>("dbo.spTransaction_GetById", new { Id = id });
        return result.FirstOrDefault();
    }

    public Task InsertTransactionAsync(TransactionEntity currency) =>
        _sqlDataAccess.SaveData("dbo.spTransaction_Insert", currency);

    public Task UpdateTransactionAsync(TransactionEntity currency) => 
        _sqlDataAccess.SaveData("dbo.spTransaction_Update", currency);

    public Task DeleteTransactionAsync(int id) =>
        _sqlDataAccess.SaveData("dbo.spTransaction_Delete", new { Id = id });
}
