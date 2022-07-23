using System.Linq;
using System.Threading.Tasks;

namespace M2c.Domain.SeedWork
{

    public interface IRepository<T> where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }

        IQueryable<T> GetAll();
        T Add(T entity);

        void Update(T entity);
        T Delete(T entity);

        Task<T> GetAsync(long entityId);
    }

}
