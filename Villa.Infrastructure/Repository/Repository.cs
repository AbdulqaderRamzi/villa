using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Villa.Application.Common.Interfaces;
using Villa.Infrastructure.Data;

namespace Villa.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    private DbSet<T> _dbSet;
    private static readonly char[] separator = [','];

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<T>();
    }

    public async Task<bool> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public async Task<T?> Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool isTracked = true)
    {
        var queryable = isTracked ? _dbSet : _dbSet.AsNoTracking();
        queryable = queryable.Where(filter);

        if (!string.IsNullOrEmpty(includeProperties))
        {
            var properties = includeProperties.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (var property in properties)
            {
                queryable = queryable.Include(property);
            }
        }
        return await queryable.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> queryable = _dbSet;

        if (filter is not null)
        {
            if (!string.IsNullOrEmpty(includeProperties))
            {
                var properties = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in properties)
                {
                    queryable = queryable.Include(property);
                }

            }
        }
        return await queryable.ToArrayAsync();
    }

    public bool Remove(T entity)
    {
        _dbSet.Remove(entity);
        return true;
    }

    public bool RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        return true;
    }

}
