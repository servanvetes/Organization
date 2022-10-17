using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        int Add(T entity);

        T FirstOrDefault(Expression<Func<T, bool>>? predicate);

        T First(Expression<Func<T, bool>>? predicate);
        bool Any(Expression<Func<T, bool>>? predicate);

        int Update(T entity);

        int Remove(T entity);

        List<T> GetAll(T entity);

        IQueryable<T> GetAllQuery();

        IQueryable<T> GetWhere(Expression<Func<T, bool>>? predicate);

        ///
        ValueTask<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);


     
      //  Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        
        void RemoveRange(IEnumerable<T> entities);
    }
}
