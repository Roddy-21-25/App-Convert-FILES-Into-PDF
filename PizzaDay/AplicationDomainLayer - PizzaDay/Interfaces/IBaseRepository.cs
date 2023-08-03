using AplicationDomainLayer___PizzaDay.Entities;

namespace AplicationDomainLayer___PizzaDay.Interfaces
{
    public interface IBaseRepository<T> where T : BaseIdEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        bool Delete(T entity);
    }
}
