using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Infrastructure.Implementation
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        //private readonly static CacheTech cacheTech = CacheTech.Memory;
        //private readonly string cacheKey = $"{typeof(T)}";
        //private readonly Func<CacheTech, ICacheService> _cacheService;

        public IQueryable<T> Entity => _dbContext.Set<T>();

        public GenericRepository(ApplicationDbContext dbContext /*, Func<CacheTech, ICacheService> cacheService*/)
        {
            _dbContext = dbContext;
            //_cacheService = cacheService;
        }

        public  virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }


        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            //_cacheService(cacheTech).Remove(cacheKey);
            return entity;
        }

        public Task AddRangeAsync(IEnumerable<T> entity)
        {
            _dbContext.Set<T>().AddRangeAsync(entity);
            //_cacheService(cacheTech).Remove(cacheKey);
            return Task.CompletedTask;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            //_cacheService(cacheTech).Remove(cacheKey);
            return  entity;
        }
        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            //_cacheService(cacheTech).Remove(cacheKey);
            return Task.CompletedTask;
        }

        public Task DeleteRangeAsync(IEnumerable<T> entity)
        {
            _dbContext.Set<T>().RemoveRange(entity);
            //_cacheService(cacheTech).Remove(cacheKey);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(bool isCached = false)
        {
            //if (!_cacheService(cacheTech).TryGet(cacheKey, out IReadOnlyList<T> cachedList))
            //{
            IReadOnlyList<T> cachedList = await _dbContext
                 .Set<T>()
                 .ToListAsync();
            //    _cacheService(cacheTech).Set(cacheKey, cachedList);
            //}
            return cachedList;
        }

        public IQueryable<T> GetByQuery(Expression<Func<T, bool>> filter)
        {
            return Entity.Where(filter);
        }
    }
}
