using FluentResults;
using MediatR;

namespace CompraVentaDivisas.Application.Abastractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
