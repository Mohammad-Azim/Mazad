
using Application.Domain.Common.Entity;
using Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class GenericService<TEntity> : IDisposable, IGenericService<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<TEntity> entities;
        private bool disposed = false;

        public GenericService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            entities = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> Create(TEntity studentDetails)
        {
            var result = entities.Add(studentDetails);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> Delete(int Id)
        {
            var entity = entities.SingleOrDefault(s => s.Id == Id);
            if (entity != null)
            {
                entities.Remove(entity);
                return await _dbContext.SaveChangesAsync();
            }
            return 0; // #??#
        }

        public async Task<TEntity> GetById(int Id)
        {
            return await entities.AsNoTracking().Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            entities.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        // CA1816: Call GC.SuppressFinalize correctly
        protected virtual void Dispose(bool disposing)
        {
            if (disposed && _dbContext != null)
            {
                _dbContext.Dispose();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~GenericService()
        {
            Dispose(false);
        }
    }
}