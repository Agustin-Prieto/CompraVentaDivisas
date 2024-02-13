using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Domain.Entities;

namespace CompraVentaDivisas.Infrastructure.Repositories;

public sealed class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly ISqlDataAccess _sqlDataAccess;

    public ExchangeRateRepository(ISqlDataAccess sqlDataAccess)
    {
        _sqlDataAccess = sqlDataAccess;
    }

    public Task<IEnumerable<ExchangeRateEntity>> GetAllExchangeRatesAsync() =>
        _sqlDataAccess.LoadData<ExchangeRateEntity, dynamic>("dbo.spExchangeRate_GetAll", new { });

    public async Task<ExchangeRateEntity> GetExchangeRateByIdAsync(int id)
    {
        var result = await _sqlDataAccess.LoadData<ExchangeRateEntity, dynamic>("dbo.spGetExchangeRateById", new { Id = id });
        return result.FirstOrDefault();
    }

    public Task InsertExchangeRateAsync(ExchangeRateEntity currency) =>
        _sqlDataAccess.SaveData("dbo.spInsertExchangeRate", currency);

    public Task UpdateExchangeRatesAsync(ExchangeRateEntity currency) =>
        _sqlDataAccess.SaveData("dbo.spUpdateExchangeRate", currency);
    public Task DeleteExchangeRatesAsync(int id) => 
        _sqlDataAccess.SaveData("dbo.spDeleteExchangeRate", new { Id = id });
}
