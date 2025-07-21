using System.Text.Json.Serialization;

namespace Ecommerce.Models;

public class OrderItem
{
    public int Id { get; set; }

    [JsonIgnore]
    public int? OrderId { get; set; }

    [JsonIgnore]
    public Order? Order { get; set; }

    public int ProductVariantId { get; set; }

    [JsonIgnore]
    public ProductVariant? ProductVariant { get; set; }

    public int Quantity { get; set;}
    public float UnitPrice { get; set; }
    
    

}