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
            libraryDbContext.book.Include(k=> k.bookType).Include(k => k.genreID);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProps = null)
        {
            IQueryable<T> query = dbSet;

            if (!String.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            if (!String.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
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
