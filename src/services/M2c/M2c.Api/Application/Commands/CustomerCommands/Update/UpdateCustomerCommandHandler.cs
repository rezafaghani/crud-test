using System;
using System.Threading;
using System.Threading.Tasks;
using M2c.Domain.AggregatesModel;
using M2c.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace M2c.Api.Application.Commands.CustomerCommands.Update
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            //check if the customer is duplicated
            var customerIsDuplicate = await _repository.GetAll().FirstOrDefaultAsync(x =>
                x.Firstname.Equals(request.Firstname) && x.Lastname.Equals(request.Lastname) &&
                x.DateOfBirth.Date == request.DateOfBirth.Date, cancellationToken: cancellationToken);
            if (customerIsDuplicate == null)
            {
                throw new DomainException("Customer information is not found");
            }

            _repository.Update(new Customer
            {
                Deleted = false,
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                PhoneNumber = request.PhoneNumber,
                BankAccountNumber = request.BankAccountNumber,
                DateOfBirth = request.DateOfBirth,
                CreateDateTime = DateTime.Now
            });
            var result = await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return result;
        }
    }
}