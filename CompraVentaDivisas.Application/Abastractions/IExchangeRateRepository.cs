using CompraVentaDivisas.Domain.Entities;

namespace CompraVentaDivisas.Application.Abastractions;

public interface IExchangeRateRepository
{
    Task<IEnumerable<ExchangeRateEntity>> GetAllExchangeRatesAsync();
    Task<ExchangeRateEntity> GetExchangeRateByIdAsync(int id);
    Task InsertExchangeRateAsync(ExchangeRateEntity currency);
    Task UpdateExchangeRatesAsync(ExchangeRateEntity currency);
    Task DeleteExchangeRatesAsync(int id);
}
