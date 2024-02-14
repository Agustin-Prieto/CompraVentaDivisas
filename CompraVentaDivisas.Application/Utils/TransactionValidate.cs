using Azure.Core;
using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Application.Features.Transaction.Commands.CreateNewTransaction;
using FluentResults;

namespace CompraVentaDivisas.Application.Utils;

public class TransactionValidate : ITransactionValidate
{
    private readonly IClientRepository _clientRepository;

    public TransactionValidate(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }
    public async Task<Result<bool>> ValidateTransaction(CreateTransactionCommand transaction)
    {
        var clientMonthlyAmountPurchased = await _clientRepository.GetClientMontlhyPurchasedAmountAsync(transaction.ClientId);

        if (clientMonthlyAmountPurchased + transaction.AmountOperated > 200)
            return Result.Fail<bool>("El cliente supera el monto maximo de compra");

        // Validar que no sea fin de semana
        if (!IsWeekday.Get(transaction.Date))
            return Result.Fail<bool>("No se puede operar en fin de semana");

        return Result.Ok(true);
    }
}
