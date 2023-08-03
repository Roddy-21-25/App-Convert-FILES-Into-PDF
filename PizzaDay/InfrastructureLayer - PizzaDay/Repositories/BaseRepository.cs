using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Interfaces;
using InfrastructureLayer___PizzaDay.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer___PizzaDay.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseIdEntity
    {
        private readonly PIZZAContext _contextPizza;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(PIZZAContext contextPizza)
        {
            _contextPizza = contextPizza;
            _dbSet = contextPizza.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public bool Delete(T entity)
        {
            _dbSet.Remove(entity);
            return true;
        }
    }
}
