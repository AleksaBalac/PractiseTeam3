using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext AppDbContext { get; set; }

        protected RepositoryBase(AppDbContext appDbContext)
        {
            this.AppDbContext = appDbContext;
        }

        public IEnumerable<T> FindAll()
        {
            return this.AppDbContext.Set<T>();
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.AppDbContext.Set<T>().Where(expression);
        }

        public void Create(T entity)
        {
            this.AppDbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.AppDbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.AppDbContext.Set<T>().Remove(entity);
        }

        public void Save()
        {
            this.AppDbContext.SaveChanges();
        }
    }
}
