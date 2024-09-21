




using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Entity.Data;
using ProjectManagementSystem.Entity.Entities;
using ProjectManagementSystem.Repository.Interface;

namespace ProjectManagementSystem.Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            await UpdateAsync(entity);
        }


        public async Task AddRangeAsync(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public IQueryable<T> GetAllPaginationAsync(int pageNumber, int pageSize)
        {
            var query = _context.Set<T>()
                .Where(a => a.IsDeleted != true)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return query;
        }

        public IQueryable<T> GetAllAsync()
        {
            return _context.Set<T>().Where(a => a.IsDeleted != true);
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(a => a.IsDeleted != true && a.ID == id);
        }

        public async Task DeleteAsync(int id)
        {
            T entity = await _context.FindAsync<T>(id);
            await DeleteAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return await Task.FromResult(entity);
        }

    }
}
