using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompraVentaDivisas.API.Abstractions;

[ApiController]
public class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender) => Sender = sender;
}
