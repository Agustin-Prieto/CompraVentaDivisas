using CompraVentaDivisas.Domain.Entities;
using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Application.Abastractions.Messaging;
using FluentResults;
using CompraVentaDivisas.Application.Features.Transaction.Queries.GetById;

namespace CompraVentaDivisas.Application.Features.Transaction.Queries.GetAll;

internal sealed class GetAllTransactionQueryHandler : IQueryHandler<GetAllTransactionQuery, IEnumerable<TransactionEntity>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetAllTransactionQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Result<IEnumerable<TransactionEntity>>> Handle(GetAllTransactionQuery request, CancellationToken cancellationToken)
    {
        var result = await _transactionRepository.GetAllTransactionsAsync();

        if (result is null)
            return Result.Fail<IEnumerable<TransactionEntity>>("No existen transacciones");

        return Result.Ok(result);
    }
}
