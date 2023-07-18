using Kutuphane.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kutuphane.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly LibraryDbContext libraryDbContext;
        internal DbSet<T> dbSet;

        public Repository(LibraryDbContext _libraryDbContext)
        {
            libraryDbContext = _libraryDbContext;
            this.dbSet = libraryDbContext.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveAtRange(IEnumerable<T> entites)
        {
            dbSet.RemoveRange(entites);
        }
    }
}
