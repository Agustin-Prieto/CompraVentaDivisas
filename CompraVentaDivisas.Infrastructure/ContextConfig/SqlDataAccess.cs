using CompraVentaDivisas.Application.Abastractions;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CompraVentaDivisas.Infrastructure.ContextConfig;

public sealed class SqlDataAccess : ISqlDataAccess
{
    public SqlConnection CreateConnection()
    {
        return new SqlConnection(ConnectionString.Get());
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters)
    {
        using var sqlConnection = CreateConnection();

        return await sqlConnection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task SaveData<T>(string storedProcedure, T parameters)
    {
        using var sqlConnection = CreateConnection();

        await sqlConnection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
