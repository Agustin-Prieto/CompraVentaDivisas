namespace CompraVentaDivisas.Infrastructure.ContextConfig;

internal sealed class ConnectionString
{
    public static string Get()
    {
        // Obtener las variables de entorno
        string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "";
        string dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "";
        string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "";
        string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "";
        string dbPassword = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD") ?? "";

        // Construir el connection string
        string connectionString = $"Server={dbHost}, {dbPort};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;Encrypt=True;";

        return connectionString;
    }
}
