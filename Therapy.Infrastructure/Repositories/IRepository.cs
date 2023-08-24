using System.Linq.Expressions;

namespace Therapy.Infrastructure.Repositories
{
  public interface IRepository<T> where T : class
  {
      Task<T> GetByIdAsync(int id);
      Task<T> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>> include);
      Task<IEnumerable<T>> GetAllAsync();
      Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> include);
      Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
      Task<T> AddAsync(T entity);
      Task UpdateAsync(T entity);
      Task DeleteAsync(int id);
  }
}
