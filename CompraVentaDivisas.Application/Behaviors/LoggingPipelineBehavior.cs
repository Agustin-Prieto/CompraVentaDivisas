using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace CompraVentaDivisas.Application.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(
        ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting request {@Request} {@DateTime}", typeof(TRequest).Name, DateTime.Now);

        var result = await next();

        _logger.LogInformation("Ending request {@Request} {@DateTime}", typeof(TRequest).Name, DateTime.Now);

        return result;
    }
}
