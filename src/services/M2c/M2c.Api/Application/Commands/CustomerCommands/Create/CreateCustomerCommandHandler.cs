using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using M2c.Domain.AggregatesModel;
using M2c.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace M2c.Api.Application.Commands.CustomerCommands.Create
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CreateCustomerCommandHandler> _logger;

        public CreateCustomerCommandHandler(ICustomerRepository repository, ILogger<CreateCustomerCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            //check if the customer is duplicated
            var customerIsDuplicate =  _repository.GetAll().Any(x =>
                x.Firstname.Equals(request.FirstName) && x.Lastname.Equals(request.LastName) &&
                x.DateOfBirth.Date == request.DateOfBirth.Date);
            if (customerIsDuplicate)
            {
                _logger.LogError("----- Customer information is duplicated: {@Customer}", request);
                throw new DomainException("Customer information is duplicated");
            }

            var customer = new Customer
            {
                Deleted = false,
                Email = request.Email,
                Firstname = request.FirstName,
                Lastname = request.LastName,
                PhoneNumber = request.PhoneNumber,
                BankAccountNumber = request.BankAccountNumber,
                DateOfBirth = request.DateOfBirth,
                CreateDateTime = DateTime.Now
            };

            _repository.Add(customer);
            _logger.LogInformation("----- Creating Customer - Customer: {@Customer}", customer);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}