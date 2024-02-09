using Microsoft.Data.SqlClient;

namespace CompraVentaDivisas.Application.Abastractions;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}
