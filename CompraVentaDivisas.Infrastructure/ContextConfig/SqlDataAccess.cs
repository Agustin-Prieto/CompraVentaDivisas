using CompraVentaDivisas.Application.Abastractions;
using Dapper;
using System.Data;

namespace CompraVentaDivisas.Infrastructure.ContextConfig;

public sealed class SqlDataAccess : ISqlDataAccess
{
    private readonly SqlConnectionFactory _sqlConnectionFactory;

    public SqlDataAccess(SqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters)
    {
        using var sqlConnection = _sqlConnectionFactory.CreateConnection();

        return await sqlConnection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(string storedProcedure, T parameters)
    {
        using var sqlConnection = _sqlConnectionFactory.CreateConnection();

        await sqlConnection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
