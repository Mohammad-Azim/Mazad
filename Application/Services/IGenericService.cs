
using Application.Domain.Entity;

namespace Application.Services
{
    public interface IGenericService<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> GetById(int Id);

        Task<TEntity> Create(TEntity entity);

        Task<int> Update(TEntity entity);

        Task<int> Delete(int Id);
    }
}