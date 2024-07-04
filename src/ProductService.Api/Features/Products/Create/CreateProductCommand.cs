using MediatR;
using ProductService.Api.Domain.Entities;

namespace ProductService.Api.Features.Products;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price) : IRequest<Guid>
{
    public Product ToProduct()
        => new Product()
        {
            Name = Name,
            Description = Description,
            Price = Price
        };
}