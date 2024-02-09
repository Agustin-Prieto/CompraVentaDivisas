using CompraVentaDivisas.API.Abstractions;
using CompraVentaDivisas.Application.Features.Test.Queries;
using CompraVentaDivisas.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompraVentaDivisas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ApiController
    {
        public TestController(ISender sender) : base(sender) { }

        [HttpGet]
        public async Task<TestEntity> Get([FromQuery] GetTestQuery getTestQuery, CancellationToken cancellationToken)
        {
            var result = await Sender.Send(getTestQuery, cancellationToken);

            return result.Value;
        }
    }
}
