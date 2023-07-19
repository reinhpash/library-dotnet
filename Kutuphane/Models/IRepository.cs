using System.Linq.Expressions;

namespace Kutuphane.Models
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProps = null);
        T Get(Expression<Func<T,bool>> filter, string? includeProps = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveAtRange(IEnumerable<T> entites);
    }
}
