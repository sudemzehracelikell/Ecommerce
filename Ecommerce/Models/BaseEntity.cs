namespace Ecommerce.Models;

public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
}

public interface IBaseEntity
{
    public int Id { get; set; }
}