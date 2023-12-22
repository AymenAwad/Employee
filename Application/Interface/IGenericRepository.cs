using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Entity { get; }

        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> GetAllAsync(bool isCached = false);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);

        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entity);

        IQueryable<T> GetByQuery(Expression<Func<T, bool>> filter);
    }
}
