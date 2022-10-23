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

       
    }
}
