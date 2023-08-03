using AplicationDomainLayer___PizzaDay.Entities;

namespace AplicationDomainLayer___PizzaDay.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Pizza> _pizza { get; }
        IGetByRepository _getByRepository { get; }
        ISignUpRepository _signUpRepository { get; }
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
