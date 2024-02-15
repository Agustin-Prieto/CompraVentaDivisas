using CompraVentaDivisas.Application.Abastractions.Messaging;
using CompraVentaDivisas.Domain.Entities;
using MediatR;

namespace CompraVentaDivisas.Application.Features.Transaction.Queries.GetByClientId;

public sealed record GetTransactionsByClientIdQuery(Guid ClientId) : IQuery<IEnumerable<TransactionEntity>>;
