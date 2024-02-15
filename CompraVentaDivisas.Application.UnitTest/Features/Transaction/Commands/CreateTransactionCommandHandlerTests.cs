using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Application.DTOs;
using CompraVentaDivisas.Application.Features.Transaction.Commands.CreateNewTransaction;
using CompraVentaDivisas.Application.Utils;
using CompraVentaDivisas.Domain.Enums;
using FluentAssertions;
using FluentResults;
using Moq;

namespace CompraVentaDivisas.Application.UnitTest.Features.Transaction.Commands;

public class CreateTransactionCommandHandlerTests
{
    private readonly Mock<ITransactionRepository> _transactionRepository;
    private readonly Mock<ITransactionValidate> _transactionValidate;
    private readonly Mock<IClientRepository> _clientRepository;

    public CreateTransactionCommandHandlerTests()
    {
        _transactionRepository = new();
        _transactionValidate = new();
        _clientRepository = new();
    }

    [Theory]
    [MemberData(nameof(GetData), parameters: 3)]
    public async Task Handle_Should_ReturnFailure_WhenAmountOperatedExceedsLimit(Guid id, decimal amountOperated, decimal AmountInPesos, DateTime date, TransactionType type, Guid currencyId, Guid exchangeRateId, Guid clientId)
    {
        // Arrange
        var command = new CreateTransactionCommand(id, amountOperated, AmountInPesos, date, type, currencyId, exchangeRateId, clientId);

        _clientRepository.Setup(
            x => x.GetClientMontlhyPurchasedAmountAsync(It.IsAny<Guid>())).ReturnsAsync(191);

        var transVal = new TransactionValidate(_clientRepository.Object);

        var handler = new CreateTransactionCommandHandler(
            transVal,
            _transactionRepository.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Reasons.First().Message.Should().Be("El cliente supera el monto maximo de compra");
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenTransactionDateIsOnWeekend()
    {
        // Arrange
        var command = new CreateTransactionCommand(default, default, default, new DateTime(2024, 02, 10), default, default, default, default);

        _clientRepository.Setup(
            x => x.GetClientMontlhyPurchasedAmountAsync(It.IsAny<Guid>())).ReturnsAsync(1);

        var transVal = new TransactionValidate(_clientRepository.Object);

        var handler = new CreateTransactionCommandHandler(
            transVal,
            _transactionRepository.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Reasons.First().Message.Should().Be("No se puede operar en fin de semana");
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenTransactionInsertFails()
    {
        // Arrange
        var command = new CreateTransactionCommand(default, default, default, default, default, default, default, default);

        _transactionValidate.Setup(
            x => x.ValidateTransaction(It.IsAny<CreateTransactionCommand>())).ReturnsAsync(Result.Ok());

        _transactionRepository.Setup(
            x => x.InsertTransactionAsync(It.IsAny<TransactionDto>())).ThrowsAsync(new Exception("Error de base de datos"));

        var handler = new CreateTransactionCommandHandler(
            _transactionValidate.Object,
            _transactionRepository.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Reasons[0].Message.Should().Be("Error en la creación de la transacción");
        result.Reasons[1].Message.Should().Be("Error de base de datos");
    }

    [Fact]
    public async Task Handle_Should_ReturnOk_WhenTransactionInsertSuccess()
    {
        // Arrange
        var command = new CreateTransactionCommand(default, default, default, default, default, default, default, default);

        _transactionValidate.Setup(
            x => x.ValidateTransaction(It.IsAny<CreateTransactionCommand>())).ReturnsAsync(Result.Ok());

        var handler = new CreateTransactionCommandHandler(
            _transactionValidate.Object,
            _transactionRepository.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    public static IEnumerable<object[]> GetData(int numTest)
    {
        var allData = new List<object[]>
        {
            new object[] { Guid.NewGuid(), 10, 22, DateTime.Now, TransactionType.Compra, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
            new object[] { Guid.NewGuid(), 100, 220, DateTime.Now, TransactionType.Venta, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
            new object[] { Guid.NewGuid(), 120, 232, DateTime.Now, TransactionType.Compra, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() },
        };

        return allData.Take(numTest);
    }
}
