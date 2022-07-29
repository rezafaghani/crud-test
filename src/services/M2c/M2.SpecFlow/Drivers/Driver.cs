using M2c.Api.Application.Commands.CustomerCommands.Create;
using M2c.Domain.AggregatesModel;
using Moq;

namespace M2.SpecFlow.Drivers;

public class Driver
{
    private readonly Mock<ICustomerRepository> _repository;
    private readonly CreateCustomerCommandValidator _validator;

    public Driver()
    {
        _repository = new Mock<ICustomerRepository>();
        _validator = new CreateCustomerCommandValidator();
    }

    public void CreateCustomer(Customer customer)
    {
        _repository.Setup(repo => repo.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(Task.FromResult(customer));
        _repository.Setup(repo => repo.UnitOfWork.SaveChangesAsync(default))
            .Returns(Task.FromResult(1));
    }
}