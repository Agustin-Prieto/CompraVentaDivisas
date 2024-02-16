using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Application.Abastractions.Messaging;
using CompraVentaDivisas.Domain.Entities;
using FluentResults;

namespace CompraVentaDivisas.Application.Features.Transaction.Queries.GetByClientId;

internal sealed class GetTransactionByClientIdQueryHandler : IQueryHandler<GetTransactionsByClientIdQuery, IEnumerable<TransactionEntity>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionByClientIdQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Result<IEnumerable<TransactionEntity>>> Handle(GetTransactionsByClientIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _transactionRepository.GetTransactionByClientIdAsync(request.ClientId);

        if (result is null)
            return Result.Fail<IEnumerable<TransactionEntity>>("No existen transacciones para este usuario");

        return Result.Ok(result);
    }
}
