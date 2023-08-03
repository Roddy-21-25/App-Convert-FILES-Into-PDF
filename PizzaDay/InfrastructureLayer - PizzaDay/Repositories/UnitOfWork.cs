using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Interfaces;
using InfrastructureLayer___PizzaDay.Data;

namespace InfrastructureLayer___PizzaDay.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PIZZAContext _contextPizza;
        private readonly IBaseRepository<Pizza> pizza;
        private readonly IGetByRepository getByRepository;
        private readonly ISignUpRepository signUpRepository;

        public UnitOfWork(PIZZAContext contextPizza)
        {
            _contextPizza = contextPizza;
        }

        public IBaseRepository<Pizza> _pizza => pizza ?? new BaseRepository<Pizza>(_contextPizza);

        public IGetByRepository _getByRepository => getByRepository ?? new GetByRepository(_contextPizza);

        public ISignUpRepository _signUpRepository => signUpRepository ?? new SignUpRepository(_contextPizza, _contextPizza);

        public void Dispose()
        {
            if (_contextPizza != null)
            {
                _contextPizza.Dispose();
            }
        }
        
        public async Task SaveChangesAsync()
        {
            await _contextPizza.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _contextPizza.SaveChanges();
        }
    }
}
