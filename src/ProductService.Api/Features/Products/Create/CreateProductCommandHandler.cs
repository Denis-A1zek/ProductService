using Arch.EntityFrameworkCore.UnitOfWork;
using MediatR;
using ProductService.Api.Domain.Entities;

namespace ProductService.Api.Features.Products;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IUnitOfWork unitOfWork)
    {
        _productRepository = unitOfWork.GetRepository<Product>();
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository
            .GetFirstOrDefaultAsync(p => request.ToProduct());

        if (product is not null)
            throw new Exception("Product exsist");

        var result = await _productRepository
            .InsertAsync(request.ToProduct(), cancellationToken);
        
        await _unitOfWork.SaveChangesAsync();
        return result.Entity.Id;
    }
}