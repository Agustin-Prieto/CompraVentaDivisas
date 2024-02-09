using CompraVentaDivisas.Application.Abastractions;
using CompraVentaDivisas.Application.Abastractions.Messaging;
using CompraVentaDivisas.Domain.Entities;
using Dapper;
using FluentResults;
using Microsoft.Data.SqlClient;

namespace CompraVentaDivisas.Application.Features.Test.Queries;

internal sealed class GetTestQueryHandler : IQueryHandler<GetTestQuery, TestEntity>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetTestQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<TestEntity>> Handle(GetTestQuery request, CancellationToken cancellationToken)
    {
        await using SqlConnection sqlConnection = _connectionFactory.CreateConnection();

        var response = await sqlConnection
            .QueryFirstOrDefaultAsync<TestEntity>(
                @"SELECT Id, Name
                  FROM Test
                  WHERE Id = @Id",
                new
                {
                    request.Id
                });

        if (response is null)
        {
            return Result.Fail<TestEntity>("Test not found");
        }

        return Result.Ok(response);
    }
}
