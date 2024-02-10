namespace CompraVentaDivisas.Domain.Entities;

public sealed class ClientEntity
{
    public ClientEntity(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
}
