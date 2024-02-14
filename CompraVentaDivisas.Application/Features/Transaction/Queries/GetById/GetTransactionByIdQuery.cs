using CompraVentaDivisas.Application.Abastractions.Messaging;
using CompraVentaDivisas.Domain.Entities;
using CompraVentaDivisas.Domain.Enums;
using FluentResults;

namespace CompraVentaDivisas.Application.Features.Transaction.Queries.GetById;

public sealed record GetTransactionByIdQuery(Guid TransactionId) : IQuery<TransactionEntity>;
