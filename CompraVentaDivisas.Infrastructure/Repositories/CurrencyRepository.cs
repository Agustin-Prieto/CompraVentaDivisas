using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Domain.Entities;

namespace CompraVentaDivisas.Infrastructure.Repositories;

public sealed class CurrencyRepository : ICurrencyRepository
{
    private readonly ISqlDataAccess _sqlDataAccess;

    public CurrencyRepository(ISqlDataAccess sqlDataAccess)
    {
        _sqlDataAccess = sqlDataAccess;
    }

    public Task<IEnumerable<CurrencyEntity>> GetAllCurrenciesAsync() =>
        _sqlDataAccess.LoadData<CurrencyEntity, dynamic>("dbo.spCurrency_GetAll", new { });

    public async Task<CurrencyEntity> GetCurrencyByIdAsync(int id)
    {
        var result = await _sqlDataAccess.LoadData<CurrencyEntity, dynamic>("dbo.spGetCurrencyById", new { Id = id });
        return result.FirstOrDefault();
    }

    public Task InsertCurrencyAsync(CurrencyEntity currency) =>
        _sqlDataAccess.SaveData("dbo.spInsertCurrency", new { currency.Name, currency.Symbol});

    public Task UpdateCurrencyAsync(CurrencyEntity currency) =>
        _sqlDataAccess.SaveData("dbo.spUpdateCurrency", currency);

    public Task DeleteCurrencyAsync(int id) =>
        _sqlDataAccess.SaveData("dbo.spDeleteCurrency", new { Id = id });
}
