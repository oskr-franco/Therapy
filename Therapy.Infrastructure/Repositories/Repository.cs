using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Therapy.Infrastructure.Data;

namespace Therapy.Infrastructure.Repositories {
  public class Repository<T> : IRepository<T> where T : class
  {
    private readonly TherapyDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(TherapyDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    /// <summary>
    /// Retrieves an entity of type TEntity by its ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    /// <summary>
    /// Retrieves all entities of type TEntity.
    /// </summary>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    /// <summary>
    /// Retrieves entities of type TEntity that satisfy the specified condition.
    /// </summary>
    /// <param name="predicate">The condition expressed as a lambda expression.</param>
    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    /// <summary>
    /// Retrieves entities of type TEntity that satisfy the specified condition.
    /// </summary>
    /// <param name="predicate">The condition expressed as a lambda expression.</param>
    public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
    /// <summary>
    /// Counts the number of entities of type TEntity that satisfy the specified condition.
    /// </summary>
    /// <param name="predicate">The condition expressed as a lambda expression.</param>
    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.CountAsync(predicate);
    }

    /// <summary>
    /// Checks whether any entities of type TEntity satisfy the specified condition.
    /// </summary>
    /// <param name="predicate">The condition expressed as a lambda expression.</param>

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
    }

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await SaveChangesAsync();
    }

    /// <summary>
    /// Removes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to remove.</param>
    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await SaveChangesAsync();
    }

    /// <summary>
    /// Removes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity id to remove.</param>
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            await DeleteAsync(entity);
        }
    }

    /// <summary>
    /// Saves the changes made to the repository asynchronously.
    /// </summary>
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
  }
}