﻿using CompraVentaDivisas.Application.Features.Transaction.Commands.CreateNewTransaction;
using FluentResults;

namespace CompraVentaDivisas.Application.Abastractions
{
    public interface ITransactionValidate
    {
        Task<Result> ValidateTransaction(CreateTransactionCommand transaction);
    }
}