using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteTaker.DAL
{
  public interface IRepository<TEntity>
  {
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);
  }
}