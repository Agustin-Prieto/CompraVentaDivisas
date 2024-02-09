using CompraVentaDivisas.Domain.Primitives;

namespace CompraVentaDivisas.Domain.Entities;

public sealed class TestEntity /*: Entity*/
{
    private TestEntity(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
}
