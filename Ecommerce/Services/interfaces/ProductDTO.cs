namespace Ecommerce.Services;
public class ProductDto
{
    public int Id { get; set; }
    public int Code { get; set; }
    public string? Name { get; set; }
    public float KDV { get; set; }
    public float BasePrice { get; set; }
    public int Stock { get; set; }
    public bool State { get; set; }
    public string? Description { get; set; }

    public string? BrandName { get; set; }
    public List<string>? CategoryNames { get; set; }
}
