using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Application.Abastractions.Messaging;
using FluentResults;
using CompraVentaDivisas.Application.DTOs;

namespace CompraVentaDivisas.Application.Features.Transaction.Commands.CreateNewTransaction;

internal sealed class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand>
{
    private readonly ITransactionValidate _transactionValidate;
    private readonly ITransactionRepository _transactionRepository;

    public CreateTransactionCommandHandler(
        ITransactionValidate transactionValidate,
        ITransactionRepository transactionRepository)
    {
        _transactionValidate = transactionValidate;
        _transactionRepository = transactionRepository;
    }

    public async Task<Result> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        // validar 
        var isTransactionValid = await _transactionValidate.ValidateTransaction(request);

        if (isTransactionValid.IsFailed)
            return Result.Fail(isTransactionValid.Errors);

        // crear
        var transaction = new TransactionDto(
            Guid.NewGuid(),
            request.AmountOperated,
            request.AmountInPesos,
            request.Date,
            request.Type,
            request.CurrencyId,
            request.ExchangeRateId,
            request.ClientId
            );

        try
        {
            await _transactionRepository.InsertTransactionAsync(transaction);

            var transactionResult = Result.Ok();
            transactionResult.WithSuccess("Transacción creada exitosamente");

            return transactionResult;
        }
        catch (Exception ex)
        {
            var error = Result.Fail("Error en la creación de la transacción");
            error.WithError(ex.Message);

            return error;
        }
    }
}
