using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using FluentValidation.TestHelper;
using M2c.Api.Application.Commands.CustomerCommands.Create;
using M2c.Domain.AggregatesModel;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Mc2.Test
{
    public class CreateCustomerTests
    {
        private readonly Mock<ICustomerRepository> _repository;
        private readonly CreateCustomerCommandValidator _validator;

        public CreateCustomerTests()
        {
            _repository = new Mock<ICustomerRepository>();
            _validator = new CreateCustomerCommandValidator();
        }

        [Fact]
        public async Task CreateCustomerValid_ReturnsSuccess()
        {
            var custom = FakeCustomer();
            var customer = FakeCustomerCommand();
            _repository.Setup(repo => repo.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(custom));
            _repository.Setup(repo => repo.UnitOfWork.SaveChangesAsync(default))
                .Returns(Task.FromResult(1));
            var loggerMock = new Mock<ILogger<CreateCustomerCommandHandler>>();
            var handler = new CreateCustomerCommandHandler(_repository.Object, loggerMock.Object, true);
            var cltToken = new CancellationToken();
            var result = await handler.Handle(customer, cltToken);

            //Assert
            Assert.True(result);
        }

        private Customer FakeCustomer()
        {
            var testCustomers = new Faker<Customer>()
                .StrictMode(true)

                //Basic rules using built-in generators
                .RuleFor(u => u.Firstname, (f, _) => f.Name.FirstName())
                .RuleFor(u => u.Lastname, (f, _) => f.Name.LastName())
                .RuleFor(u => u.BankAccountNumber, f => f.Finance.Account())
                .RuleFor(u => u.PhoneNumber, (f, _) => f.Phone.PhoneNumberFormat(1))
                .RuleFor(u => u.Email, (f, _) => f.Internet.Email())
                .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(u => u.UpdateDateTime, f => f.Date.Recent())
                .RuleFor(u => u.DeleteDateTime, f => f.Date.Recent())
                .RuleFor(u => u.Deleted, _ => false)
                .RuleFor(u => u.CreateDateTime, f => f.Date.Recent())
                .RuleFor(u => u.CreatedBy, f => f.Random.Long());
            var customer = testCustomers.Generate();
            return customer;
        }

        private CreateCustomerCommand FakeCustomerCommand(Dictionary<string, object> args = null)
        {
            var testCustomers = new Faker<CreateCustomerCommand>()
                .StrictMode(true)

                //Basic rules using built-in generators
                .RuleFor(u => u.FirstName, (f, _) => f.Name.FirstName())
                .RuleFor(u => u.LastName, (f, _) => f.Name.LastName())
                .RuleFor(u => u.BankAccountNumber, f => f.Finance.Account())
                .RuleFor(u => u.PhoneNumber,
                    f => args != null && args.ContainsKey("PhoneNumber")
                        ? f.Phone.PhoneNumberFormat(1)
                        : f.Random.String())
                .RuleFor(u => u.Email, (f, _) => f.Internet.Email())
                .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth);
            var customer = testCustomers.Generate();
            return customer;
        }

        [Fact]
        public async Task CreateCorruptedPhoneNumberCustomer()
        {
            var customer = FakeCustomerCommand(new Dictionary<string, object>
                { ["PhoneNumber"] = "" });
            var result =await _validator.TestValidateAsync(customer);
            result.ShouldHaveValidationErrorFor(person => person.PhoneNumber);
        }
    }
}