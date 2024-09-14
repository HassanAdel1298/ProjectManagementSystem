

using ProjectManagementSystem.Entity.Entities;

namespace ProjectManagementSystem.Repository.Interface
{
    public interface IRepository<T> where T : BaseModel, new()
    {
       
        IQueryable<T> GetAll();
        T GetByID(int id);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        public void AddRange(List<T> entities);
        void SaveChanges();
    }
}
