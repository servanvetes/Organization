using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly OrganizationContext dbContext;

        protected DbSet<T> _entity => dbContext.Set<T>();

        public GenericRepository(OrganizationContext context)
        {
            dbContext = context;
        }

        public virtual int Add(T entity)
        {
            _entity.Add(entity);
            return dbContext.SaveChanges();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>>? predicate)
        {

            return _entity.Where(predicate).FirstOrDefault();
        }
        public T First(Expression<Func<T, bool>>? predicate)
        {
            return _entity.Where(predicate).First();
        }
        public virtual bool Any(Expression<Func<T, bool>>? predicate)
        {
            return _entity.Any(predicate);
        }

        public virtual int Update(T entity)
        {
            _entity.Update(entity);
            return dbContext.SaveChanges();
        }

        public virtual int Remove(T entity)
        {

            _entity.Remove(entity);
            return dbContext.SaveChanges();
        }

        public virtual List<T> GetAll(T entity)
        {
            return _entity.ToList();
        }
        IQueryable<T> IGenericRepository<T>.GetAllQuery()
        {
            return _entity.Select(t => t);
        }

        IQueryable<T> IGenericRepository<T>.GetWhere(Expression<Func<T, bool>>? predicate)
        {
            return _entity.Where(predicate); ;
        }
        ///
        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public ValueTask<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }



        public void RemoveRange(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

     
    }
}
