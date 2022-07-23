using System.Net;
using System.Threading.Tasks;
using M2c.Api.Application.Commands.CustomerCommands.Create;
using M2c.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace M2c.Api.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CustomerController : ControllerBase
    {


        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;
        public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [Route("create")]
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> CreateOrderDraftFromBasketDataAsync([FromBody] CreateCustomerCommand request)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - :{FirstName} - {LastName} - {DateOfBirth} ({@Command})",
                request.GetGenericTypeName(),
                request.Firstname,
                request.Lastname,
                request.DateOfBirth,
                request);

            return await _mediator.Send(request).ConfigureAwait(false);
        }

    }
}