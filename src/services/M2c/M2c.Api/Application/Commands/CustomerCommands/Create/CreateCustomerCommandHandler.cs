using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using M2c.Domain.AggregatesModel;
using M2c.Domain.Exceptions;
using MediatR;

namespace M2c.Api.Application.Commands.CustomerCommands.Create
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _repository;

        public CreateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            //check if the customer is duplicated
            var customerIsDuplicate =  _repository.GetAll().Any(x =>
                x.Firstname.Equals(request.FirstName) && x.Lastname.Equals(request.LastName) &&
                x.DateOfBirth.Date == request.DateOfBirth.Date);
            if (customerIsDuplicate)
            {
                throw new DomainException("Customer information is duplicated");
            }

            _repository.Add(new Customer
            {
                Deleted = false,
                Email = request.Email,
                Firstname = request.FirstName,
                Lastname = request.LastName,
                PhoneNumber = request.PhoneNumber,
                BankAccountNumber = request.BankAccountNumber,
                DateOfBirth = request.DateOfBirth,
                CreateDateTime = DateTime.Now
            });
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}