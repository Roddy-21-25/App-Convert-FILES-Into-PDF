using AplicationDomainLayer___PizzaDay.Entities;

namespace AplicationDomainLayer___PizzaDay.Interfaces
{
    public interface ISignUpRepository : IBaseRepository<Chef>
    {
        Task<Chef> SignUp(Chef chef);
    }
}