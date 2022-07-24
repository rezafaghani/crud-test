using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using M2c.Api.Application.Commands.CustomerCommands.Create;
using M2c.Api.Application.Commands.CustomerCommands.Update;
using M2c.Api.Application.Queries;
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
        private readonly ICustomerQueries _customerQueries;

        public CustomerController(ILogger<CustomerController> logger, IMediator mediator,
            ICustomerQueries customerQueries)
        {
            _logger = logger;
            _mediator = mediator;
            _customerQueries = customerQueries;
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> CreateCustomer([FromBody] CreateCustomerCommand request)
        {
            _logger.LogInformation(
                "----- Sending command: {CommandName} - :{FirstName} - {LastName} - {DateOfBirth} ({@Command})",
                request.GetGenericTypeName(),
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                request);

            return await _mediator.Send(request).ConfigureAwait(false);
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> UpdateCustomer([FromBody] UpdateCustomerCommand request)
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

        [Route("getCustomers")]
        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerSummary>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetOrderAsync([FromQuery] string firstName, [FromQuery] string lastname)
        {
            try
            {
                var customers = await _customerQueries.GetCustomersAsync(firstName, lastname);

                return Ok(customers);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}