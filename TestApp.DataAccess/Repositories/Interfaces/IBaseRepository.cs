using Ardalis.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestApp.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }

        Task<T> CreateAsync(T item);
        Task Update(T item);
        Task Delete(T item); 
        Task<IEnumerable<T>> GetAllAsync();
        
        //Task<T> GetSingleAsync(ISpecification<T> specification);
        //Task<IEnumerable<T>> GetManyAsync(ISpecification<T> specification);
    }
}
