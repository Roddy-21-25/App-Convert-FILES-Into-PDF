using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Interfaces;
using AplicationDomainLayer___PizzaDay.PaginationEntity;

namespace AplicationDomainLayer___PizzaDay.Services
{
    public class PizzaServices : IPizzaServices
    {
        private readonly IUnitOfWork _pizzaUnitOfWork;
        public PizzaServices(IUnitOfWork pizzaUnitOfWork)
        {
            _pizzaUnitOfWork = pizzaUnitOfWork;
        }

        public PaginationListLogic<Pizza> GetAll(PaginationValues values)
        {
            var Pizzas =  _pizzaUnitOfWork._pizza.GetAll();
            
            var PizzaPagination = PaginationListLogic<Pizza>.PaginationImplementation(Pizzas, values.PageToShow, values.ElementToShow);

            return PizzaPagination;
        }
        public async Task<Pizza> GetById(int id)
        {
            return await _pizzaUnitOfWork._pizza.GetById(id);
        }
        public void Insert(Pizza entity)
        {
            _pizzaUnitOfWork._pizza.Insert(entity);
            _pizzaUnitOfWork.SaveChanges();
        }
        public void Update(Pizza entity)
        {
            _pizzaUnitOfWork._pizza.Update(entity);
            _pizzaUnitOfWork.SaveChanges();
        }
        public bool Delete(Pizza entity)
        {
            var response = _pizzaUnitOfWork._pizza.Delete(entity);
            _pizzaUnitOfWork.SaveChanges();
            return response;
        }
    }
}
