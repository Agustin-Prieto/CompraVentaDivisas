using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Application.DTOs;
using CompraVentaDivisas.Domain.Entities;
using Dapper;

namespace CompraVentaDivisas.Infrastructure.Repositories;

public sealed class TransactionRepository : ITransactionRepository
{
    private readonly ISqlDataAccess _sqlDataAccess;

    public TransactionRepository(ISqlDataAccess sqlDataAccess)
    {
        _sqlDataAccess = sqlDataAccess;
    }

    public async Task<IEnumerable<TransactionEntity>> GetAllTransactionsAsync() 
    {
        using var sqlConnection = _sqlDataAccess.CreateConnection();

        var result = await sqlConnection.QueryAsync<
            TransactionEntity,
            CurrencyEntity,
            ExchangeRateEntity,
            ClientEntity,
            TransactionEntity>("dbo.spTransaction_GetAll", (transaction, currency, exchangeRate, client) =>
            {
                transaction.Currency = currency;
                transaction.ExchangeRate = exchangeRate;
                transaction.Client = client;
                return transaction;
            },
        splitOn: "CurrencyId, ExchangeRateId, ClientId",
        commandType: System.Data.CommandType.StoredProcedure);

        return result;
    }


    public async Task<TransactionEntity> GetTransactionByIdAsync(Guid id)
    {
        using var sqlConnection = _sqlDataAccess.CreateConnection();

        var result =  await sqlConnection.QueryAsync<
            TransactionEntity, 
            CurrencyEntity, 
            ExchangeRateEntity, 
            ClientEntity, 
            TransactionEntity>("dbo.spTransaction_GetById", (transaction, currency, exchangeRate, client) =>
        {
            transaction.Currency = currency;
            transaction.ExchangeRate = exchangeRate;
            transaction.Client = client;
            return transaction;
        },
        new { id },
        splitOn: "CurrencyId, ExchangeRateId, ClientId",
        commandType: System.Data.CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<TransactionEntity>> GetTransactionByClientIdAsync(Guid clientId)
    {
        using var sqlConnection = _sqlDataAccess.CreateConnection();

        var result = await sqlConnection.QueryAsync<
            TransactionEntity,
            CurrencyEntity,
            ExchangeRateEntity,
            ClientEntity,
            TransactionEntity>("dbo.spTransaction_GetByClientId", (transaction, currency, exchangeRate, client) =>
            {
                transaction.Currency = currency;
                transaction.ExchangeRate = exchangeRate;
                transaction.Client = client;
                return transaction;
            },
        new { clientId },
        splitOn: "CurrencyId, ExchangeRateId, ClientId",
        commandType: System.Data.CommandType.StoredProcedure);

        return result;
    }

    public Task InsertTransactionAsync(TransactionDto transaction) =>
        _sqlDataAccess.SaveData("dbo.spTransaction_Insert", transaction);

    public Task UpdateTransactionAsync(TransactionEntity transaction) => 
        _sqlDataAccess.SaveData("dbo.spTransaction_Update", transaction);

    public Task DeleteTransactionAsync(Guid id) =>
        _sqlDataAccess.SaveData("dbo.spTransaction_Delete", new { Id = id });
}
