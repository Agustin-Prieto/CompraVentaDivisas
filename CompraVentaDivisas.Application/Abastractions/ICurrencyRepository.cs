using CompraVentaDivisas.Domain.Entities;

namespace CompraVentaDivisas.Application.Abastractions;

public interface ICurrencyRepository
{
    Task<IEnumerable<CurrencyEntity>> GetAllCurrenciesAsync();
    Task<CurrencyEntity> GetCurrencyByIdAsync(int id);
    Task InsertCurrencyAsync(CurrencyEntity currency);
    Task UpdateCurrencyAsync(CurrencyEntity currency);
    Task DeleteCurrencyAsync(int id);
}
