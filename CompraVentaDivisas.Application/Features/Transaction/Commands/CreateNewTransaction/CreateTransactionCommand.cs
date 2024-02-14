using CompraVentaDivisas.Application.Abastractions.Messaging;
using CompraVentaDivisas.Domain.Enums;

namespace CompraVentaDivisas.Application.Features.Transaction.Commands.CreateNewTransaction;

public sealed record CreateTransactionCommand(
    Guid Id,
    decimal AmountOperated,
    decimal AmountInPesos,
    DateTime Date,
    TransactionType Type,
    Guid CurrencyId,
    Guid ExchangeRateId,
    Guid ClientId) : ICommand;
