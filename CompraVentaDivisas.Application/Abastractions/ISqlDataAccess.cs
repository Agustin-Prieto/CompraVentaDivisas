using Microsoft.Data.SqlClient;

namespace CompraVentaDivisas.Application.Abastractions;

public interface ISqlDataAccess
{
    SqlConnection CreateConnection();
    Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters);
    Task<T> LoadDataSingle<T, U>(string sql, U parameters);

    Task SaveData<T>(string sql, T parameters);
}
