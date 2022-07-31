using System;
using System.Threading;
using System.Threading.Tasks;
using M2c.Domain.AggregatesModel;
using M2c.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace M2c.Api.Application.Commands.CustomerCommands.Update
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly ILogger<UpdateCustomerCommandHandler> _logger;
        private readonly ICustomerRepository _repository;

        public UpdateCustomerCommandHandler(ICustomerRepository repository,
            ILogger<UpdateCustomerCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var dateOfBirth = DateTime.Parse(request.DateOfBirth).Date;
            //check if the customer is duplicated
            Customer customerIsDuplicate = await _repository.GetAll().FirstOrDefaultAsync(x =>
                x.Firstname.Equals(request.Firstname) && x.Lastname.Equals(request.Lastname) &&
                x.DateOfBirth.Date == dateOfBirth, cancellationToken);
            if (customerIsDuplicate == null) throw new DomainException("Customer information is not found");

            Customer customer = new()
            {
                Deleted = false,
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                PhoneNumber = request.PhoneNumber,
                BankAccountNumber = request.BankAccountNumber,
                DateOfBirth = dateOfBirth,
                UpdateDateTime = DateTime.Now
            };
            _repository.Update(customer);
            _logger.LogInformation("----- Updating Customer - Customer: {@Customer}", customer);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}