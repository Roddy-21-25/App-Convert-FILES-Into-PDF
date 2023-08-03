using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.PaginationEntity;

namespace AplicationDomainLayer___PizzaDay.Interfaces
{
    public interface IPizzaServices
    {
        bool Delete(Pizza entity);
        PaginationListLogic<Pizza> GetAll(PaginationValues values);
        Task<Pizza> GetById(int id);
        void Insert(Pizza entity);
        void Update(Pizza entity);
    }
}