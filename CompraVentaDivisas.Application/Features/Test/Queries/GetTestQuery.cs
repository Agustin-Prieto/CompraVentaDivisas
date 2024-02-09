using CompraVentaDivisas.Application.Abastractions.Messaging;
using CompraVentaDivisas.Domain.Entities;

namespace CompraVentaDivisas.Application.Features.Test.Queries;

public sealed record GetTestQuery(Guid Id) : IQuery<TestEntity>;
