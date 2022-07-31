#nullable enable
using System;
using System.Linq;
using System.Threading.Tasks;
using M2c.Domain.AggregatesModel;
using M2c.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace M2c.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository

    {
        private readonly M2CDbContext _context;

        public CustomerRepository(M2CDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public IQueryable<Customer> GetAll()
        {
            return _context.Set<Customer>().Where(x => x.Deleted == false).AsNoTracking().AsQueryable();
        }

        public Customer Add(Customer entity)
        {
            return _context.Set<Customer>()
                .Add(entity)
                .Entity;
        }


        public void Update(Customer entity)
        {
            entity.SetUpdateDateTime();
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Customer Delete(Customer entity)
        {
            entity.SetDeleteDateTime();
            entity.SetDeleted();
            return _context.Set<Customer>()
                .Update(entity)
                .Entity;
        }

        public async Task<Customer?> GetAsync(string firstname, string lastname)
        {
            Customer? customer = await _context
                .Set<Customer>()
                .FirstOrDefaultAsync(o => o.Firstname == firstname && o.Lastname == lastname);
            if (customer == null)
                customer = _context
                    .Set<Customer>()
                    .Local
                    .FirstOrDefault(o => o.Firstname == firstname && o.Lastname == lastname);


            return customer;
        }
    }
}