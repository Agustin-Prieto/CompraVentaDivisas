using CompraVentaDivisas.Application.Abastractions.Messaging;
using CompraVentaDivisas.Domain.Entities;

namespace CompraVentaDivisas.Application.Features.Transaction.Queries.GetById;

public sealed record GetTransactionByIdQuery(Guid TransactionId) : IQuery<TransactionEntity>;
