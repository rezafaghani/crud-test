#nullable enable
using System;
using System.Linq;
using M2c.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace M2c.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : Entity
    {
        private readonly M2CDbContext _context;

        public Repository(M2CDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().Where(x => x.Deleted == false).AsNoTracking().AsQueryable();
        }

        public T Add(T entity)
        {
            return _context.Set<T>()
                .Add(entity)
                .Entity;
        }


        public void Update(T entity)
        {
            entity.SetUpdateDateTime();
            _context.Entry(entity).State = EntityState.Modified;
        }

        public T Delete(T entity)
        {
            entity.SetDeleteDateTime();
            entity.SetDeleted();
            return _context.Set<T>()
                .Update(entity)
                .Entity;
        }


        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}