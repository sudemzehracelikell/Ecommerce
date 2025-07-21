using System.Text.Json.Serialization;

namespace Ecommerce.Models;

public class ProductVariant
{

    public int Id { get; set; }
    public int ProductId { get; set; }

    [JsonIgnore]
    public Product? Product { get; set; }

    [JsonIgnore]
    public ICollection<Variant>? Variants { get; set; }

    public float Price { get; set; }
    public int Stock { get; set; }

    [JsonIgnore]
    public ICollection<OrderItem>? OrderItems { get; set; }

}