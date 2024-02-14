using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Domain.Entities;

namespace CompraVentaDivisas.Infrastructure.Repositories;

public sealed class ClientRepository : IClientRepository
{
    private readonly ISqlDataAccess _sqlDataAccess;

    public ClientRepository(ISqlDataAccess sqlDataAccess)
    {
        _sqlDataAccess = sqlDataAccess;
    }

    public Task<decimal?> GetClientMontlhyPurchasedAmountAsync(Guid id) =>
        _sqlDataAccess.LoadDataSingle<decimal?, dynamic>("dbo.spClient_GetMontlhyPurchaseAmount", new { ClientId = id });
}
