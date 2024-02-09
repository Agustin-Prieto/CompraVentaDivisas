using CompraVentaDivisas.Application.Abastractions;
using Microsoft.Data.SqlClient;

namespace CompraVentaDivisas.Infrastructure.ContextConfig;

public sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    public SqlConnection CreateConnection()
    {
        return new SqlConnection(ConnectionString.Get());
    }
}
