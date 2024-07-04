namespace ProductService.Api.Domain.Entities;

public class Order : Entity
{
    public List<Product> Products { get; set; } = new();
    
}