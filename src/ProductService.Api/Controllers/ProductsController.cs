using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Api.Features.Products;

namespace ProductService.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : BaseController
{
    public ProductsController(IMediator mediator) : base(mediator){}

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]CreateProductCommand request)
        => Ok(await Mediator.Send(request));
}