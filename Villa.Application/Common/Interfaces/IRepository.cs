using System.Linq.Expressions;

namespace Villa.Application.Common.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool isTracked = true);
    Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
    Task<bool> Add(T entity);
    bool Remove(T entity);
    bool RemoveRange(IEnumerable<T> entities);
}
