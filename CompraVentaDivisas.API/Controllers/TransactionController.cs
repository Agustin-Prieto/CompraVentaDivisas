using CompraVentaDivisas.API.Abstractions;
using CompraVentaDivisas.Application.Features.Transaction.Commands.CreateNewTransaction;
using CompraVentaDivisas.Application.Features.Transaction.Queries.GetAll;
using CompraVentaDivisas.Application.Features.Transaction.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CompraVentaDivisas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ApiController
    {
        public TransactionController(ISender sender) : base(sender)
        {
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var response = await Sender.Send(new GetAllTransactionQuery(), cancellationToken);
            return Ok(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] GetTransactionByIdQuery command, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);
            return Ok(response);
        }

        [HttpPost("CreateTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command, CancellationToken cancellationToken)
        {
            var response = await Sender.Send(command, cancellationToken);
            return Ok(response);
        }
    }
}
