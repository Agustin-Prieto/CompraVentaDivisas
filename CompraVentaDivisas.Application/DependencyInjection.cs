using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Application.Behaviors;
using CompraVentaDivisas.Application.Utils;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CompraVentaDivisas.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(LoggingPipelineBehavior<,>));

        services.AddScoped<ITransactionValidate, TransactionValidate>();

        return services;
    }
}

