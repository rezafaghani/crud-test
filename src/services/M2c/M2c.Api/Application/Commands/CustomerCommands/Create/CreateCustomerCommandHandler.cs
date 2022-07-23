using System;
using System.Threading;
using System.Threading.Tasks;
using M2c.Domain.AggregatesModel;
using M2c.Domain.Exceptions;
using M2c.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace M2c.Api.Application.Commands.CustomerCommands.Create
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, bool>
    {
        private readonly IRepository<Customer> _repository;

        public CreateCustomerCommandHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            //check if the customer is duplicated
            var customerIsDuplicate = await _repository.GetAll().AnyAsync(x =>
                x.Firstname.Equals(request.Firstname) && x.Lastname.Equals(request.Lastname) &&
                x.DateOfBirth.Date == request.DateOfBirth.Date, cancellationToken: cancellationToken);
            if (customerIsDuplicate)
            {
                throw new DomainException("Customer information is duplicated");
            }

            _repository.Add(new Customer
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
            var result= await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return result;
        }
    }
}