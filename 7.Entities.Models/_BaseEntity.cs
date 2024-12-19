namespace _7.Entities.Models;

public class BaseEntity
{
    public string? Id { get; set; } = null!;
    public int? IsDeleted { get; set; }
}


public class IdOnly
{
    public int id { get; set; }
}