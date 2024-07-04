using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Api.Controllers;

public class BaseController : Controller
{
    protected IMediator Mediator { get; private set; }

    public BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }
}