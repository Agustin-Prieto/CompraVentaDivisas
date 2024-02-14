using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Infrastructure.ContextConfig;
using CompraVentaDivisas.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CompraVentaDivisas.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}
