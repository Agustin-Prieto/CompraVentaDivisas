namespace CompraVentaDivisas.Application.Abastractions;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters);

    Task SaveData<T>(string sql, T parameters);
}
