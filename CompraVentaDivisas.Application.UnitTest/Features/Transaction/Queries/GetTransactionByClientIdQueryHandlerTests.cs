using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Application.Features.Transaction.Queries.GetByClientId;
using CompraVentaDivisas.Domain.Entities;
using CompraVentaDivisas.Domain.Enums;
using FluentAssertions;
using Moq;

namespace CompraVentaDivisas.Application.UnitTest.Features.Transaction.Queries;

public class GetTransactionByClientIdQueryHandlerTests
{
    private readonly Mock<ITransactionRepository> _transactionRepository;

    public GetTransactionByClientIdQueryHandlerTests()
    {
        _transactionRepository = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenTransactionsNotFound()
    {
        // Arrange
        var query = new GetTransactionsByClientIdQuery(default);
        _transactionRepository.Setup(x => x.GetTransactionByClientIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as IEnumerable<TransactionEntity>);

        var handler = new GetTransactionByClientIdQueryHandler(_transactionRepository.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Reasons.First().Message.Should().Be("No existen transacciones para este usuario");
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public async Task Handle_Should_ReturnSuccess_WhenTransactionsFound(IEnumerable<TransactionEntity> transactionEntities)
    {
        // Arrange
        var query = new GetTransactionsByClientIdQuery(default);
        _transactionRepository.Setup(x => x.GetTransactionByClientIdAsync(It.IsAny<Guid>())).ReturnsAsync(transactionEntities);

        var handler = new GetTransactionByClientIdQueryHandler(_transactionRepository.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }

    public static IEnumerable<object[]> GetData()
    {
        yield return new object[] { new List<TransactionEntity>() { new TransactionEntity(Guid.NewGuid(), 10, 100, DateTime.Now, TransactionType.Compra) } };
        yield return new object[] { new List<TransactionEntity>() { new TransactionEntity(Guid.NewGuid(), 100, 10, DateTime.Now, TransactionType.Venta) } };
        yield return new object[] { new List<TransactionEntity>() { new TransactionEntity(Guid.NewGuid(), 100, 100, DateTime.Now, TransactionType.Compra) } };
    }
}
