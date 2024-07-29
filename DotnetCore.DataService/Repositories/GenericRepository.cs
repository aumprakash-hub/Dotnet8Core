using DotnetCore.DataService.Data;
using DotnetCore.DataService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DotnetCore.DataService.Repositories;

public class GenericRepository<T>(AppDbContext context, ILogger logger) : IGenericRepository<T>
    where T : class
{
    protected readonly AppDbContext Context = context;
    private readonly ILogger _logger = logger;
    internal readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();    }

    public  virtual async Task<T?> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public  virtual async Task<bool> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        return true;
    }

    public virtual  Task<bool> Update(T entity)
    {
        throw new NotImplementedException();
    }

    public  virtual Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}