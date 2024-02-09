using FluentResults;
using MediatR;

namespace CompraVentaDivisas.Application.Abastractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
