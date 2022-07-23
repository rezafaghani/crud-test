using System.Threading;
using System.Threading.Tasks;
using Bogus;
using M2c.Api.Application.Commands.CustomerCommands.Create;
using M2c.Domain.AggregatesModel;
using Moq;
using Xunit;

namespace Mc2.Test
{
    public class CreateCustomerTests
    {
        private readonly Mock<ICustomerRepository> _repository;

        public CreateCustomerTests()
        {
            _repository = new Mock<ICustomerRepository>();
        }
        [Fact]
        public async Task CreateCustomerValid_ReturnsSuccess()
        {
            var customer = FakeCustomerCommand();
            _repository.Setup(repo => repo.GetAsync(It.IsAny<string>(),It.IsAny<string>()))
                .Returns(Task.FromResult<Customer>(FakeCustomer()));
            _repository.Setup(repo => repo.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));
            var handler = new CreateCustomerCommandHandler( _repository.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(customer,cltToken);

            //Assert
            Assert.False(result);
        }
        private Customer FakeCustomer()
        {
            var testCustomers = new Faker<Customer>()
                .StrictMode(true)

                //Basic rules using built-in generators
                .RuleFor(u => u.Firstname, (f, _) => f.Name.FirstName())
                .RuleFor(u => u.Lastname, (f, _) => f.Name.LastName())
                .RuleFor(u => u.BankAccountNumber, f => f.Finance.Account())
                .RuleFor(u => u.PhoneNumber, (f, _) => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, (f, _) => f.Internet.Email())
                .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(u=>u.UpdateDateTime,f=>f.Date.Recent())
                .RuleFor(u=>u.DeleteDateTime,f=>f.Date.Recent())
                .RuleFor(u=>u.Deleted,f=>false)
                .RuleFor(u=>u.CreateDateTime,f=>f.Date.Recent())
                .RuleFor(u=>u.CreatedBy,f=>f.Random.Long());
            var customer = testCustomers.Generate();
            return customer;
        }
        private CreateCustomerCommand FakeCustomerCommand()
        {
            var testCustomers = new Faker<CreateCustomerCommand>()
                .StrictMode(true)

                //Basic rules using built-in generators
                .RuleFor(u => u.FirstName, (f, _) => f.Name.FirstName())
                .RuleFor(u => u.LastName, (f, _) => f.Name.LastName())
                .RuleFor(u => u.BankAccountNumber, f => f.Finance.Account())
                .RuleFor(u => u.PhoneNumber, (f, _) => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, (f, _) => f.Internet.Email())
                .RuleFor(u => u.DateOfBirth, f => f.Person.DateOfBirth);
            var customer = testCustomers.Generate();
            return customer;
        }
    }
}