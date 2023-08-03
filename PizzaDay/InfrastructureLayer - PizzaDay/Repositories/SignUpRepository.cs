using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Interfaces;
using InfrastructureLayer___PizzaDay.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer___PizzaDay.Repositories
{
    public class SignUpRepository : BaseRepository<Chef>, ISignUpRepository
    {
        private readonly PIZZAContext _contextChefV2;
        private readonly DbSet<Chef> _dbSetV2;

        public SignUpRepository(PIZZAContext contextChef, PIZZAContext contextPizza) : base(contextChef) 
        {
            _contextChefV2 = contextPizza;
            _dbSetV2 = _contextChefV2.Set<Chef>();
        }

        public async Task<Chef> SignUp(Chef chef)
        {
            return await _dbSetV2.FirstOrDefaultAsync(x => x.UserName == chef.UserName);
        }
    }
}
