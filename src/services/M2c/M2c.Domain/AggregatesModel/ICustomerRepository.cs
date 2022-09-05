using System.Linq;
using System.Threading.Tasks;
using M2c.Domain.SeedWork;

namespace M2c.Domain.AggregatesModel
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IQueryable<Customer> GetAll();
        Customer Add(Customer entity);

        void Update(Customer entity);
        Customer Delete(Customer entity);
        Task<Customer> GetAsync(string firstname, string lastname);
    }
}