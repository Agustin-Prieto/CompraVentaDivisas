using CompraVentaDivisas.Application.Abastractions.Messaging;
using CompraVentaDivisas.Domain.Entities;
using CompraVentaDivisas.Domain.Enums;
using FluentResults;

namespace CompraVentaDivisas.Application.Features.Transaction.Queries.GetAll;

public sealed record GetAllTransactionQuery() : IQuery<IEnumerable<TransactionEntity>>;
