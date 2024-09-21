

using ProjectManagementSystem.Entity.Entities;

namespace ProjectManagementSystem.Repository.Interface
{
    public interface IRepository<T> where T : BaseModel
    {
       
        IQueryable<T> GetAllAsync();
        IQueryable<T> GetAllPaginationAsync(int pageNumber, int pageSize);
        Task<T> GetByIDAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(int id);
        Task AddRangeAsync(List<T> entities);
        Task SaveChangesAsync();
    }
}
