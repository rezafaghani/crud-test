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
        private readonly ILogger<CreateCustomerCommandHandler> _logger;
        private readonly ICustomerRepository _repository;


        public CreateCustomerCommandHandler(ICustomerRepository repository,
            ILogger<CreateCustomerCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var dateOfBirth = DateTime.Parse(request.DateOfBirth).Date;
            //check if the customer is duplicated
            bool customerIsDuplicate = _repository.GetAll().Any(x =>
                x.Firstname.Equals(request.FirstName.Trim().ToLower()) &&
                x.Lastname.Equals(request.LastName.Trim().ToLower()) &&
                x.DateOfBirth.Date == dateOfBirth);
            if (customerIsDuplicate)
            {
                _logger.LogError("----- Customer information is duplicated: {@Customer}", request);
                throw new DomainException("Customer information is duplicated");
            }

            bool customerEmailIsDuplicate = _repository.GetAll().Any(x =>
                x.Email.Equals(request.Email.Trim().ToLower()));
            if (customerEmailIsDuplicate)
            {
                _logger.LogError("----- Customer email is duplicated: {@Customer}", request);
                throw new DomainException("Customer email is duplicated");
            }

            Customer customer = new()
            {
                Deleted = false,
                Email = request.Email.Trim().ToLower(),
                Firstname = request.FirstName.Trim().ToLower(),
                Lastname = request.LastName.Trim().ToLower(),
                PhoneNumber = request.PhoneNumber.Trim(),
                BankAccountNumber = request.BankAccountNumber.Trim(),
                DateOfBirth = dateOfBirth,
                CreateDateTime = DateTime.Now
            };

            _repository.Add(customer);
            _logger.LogInformation("----- Creating Customer - Customer: {@Customer}", customer);

            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}