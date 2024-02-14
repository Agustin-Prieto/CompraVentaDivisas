using CompraVentaDivisas.Domain.Entities;
using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Application.Abastractions.Messaging;
using FluentResults;

namespace CompraVentaDivisas.Application.Features.Transaction.Queries.GetById;

internal sealed class GetAllTransactionQueryHandler : IQueryHandler<GetTransactionByIdQuery, TransactionEntity>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetAllTransactionQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Result<TransactionEntity>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _transactionRepository.GetTransactionByIdAsync(request.TransactionId);

        if (result is null)
            return Result.Fail<TransactionEntity>("La transacción no existe");

        return Result.Ok(result);
    }
}
