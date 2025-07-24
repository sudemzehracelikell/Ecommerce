using System.Text.Json.Serialization;

namespace Ecommerce.Models;

public class Variant
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    
    [JsonIgnore]
    public ICollection<ProductVariant>? ProductVariants { get; set; }
    
}