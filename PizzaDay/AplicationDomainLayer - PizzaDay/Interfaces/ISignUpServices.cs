using AplicationDomainLayer___PizzaDay.Entities;

namespace AplicationDomainLayer___PizzaDay.Interfaces
{
    public interface ISignUpServices
    {
        Task RegisterChef(Chef chef);
        Task<Chef> SignUp(Chef chef);
    }
}