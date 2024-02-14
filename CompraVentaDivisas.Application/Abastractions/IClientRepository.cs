using CompraVentaDivisas.Domain.Entities;

namespace CompraVentaDivisas.Application.Abastractions;

public interface IClientRepository
{
    Task<decimal?> GetClientMontlhyPurchasedAmountAsync(Guid id);
}
