using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Interfaces;

namespace AplicationDomainLayer___PizzaDay.Services
{
    public class SignUpServices : ISignUpServices
    {
        private readonly IUnitOfWork _chefUnitOfWork;
        public SignUpServices(IUnitOfWork chefUnitOfWork)
        {
            _chefUnitOfWork = chefUnitOfWork;
        }

        public async Task<Chef> SignUp(Chef chef)
        {
            return await _chefUnitOfWork._signUpRepository.SignUp(chef);
        }

        public async Task RegisterChef(Chef chef)
        {
            _chefUnitOfWork._signUpRepository.Insert(chef);
            await _chefUnitOfWork.SaveChangesAsync();
        }
    }
}
