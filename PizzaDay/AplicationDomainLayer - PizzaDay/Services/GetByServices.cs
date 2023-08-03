using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Exceptions;
using AplicationDomainLayer___PizzaDay.Interfaces;

namespace AplicationDomainLayer___PizzaDay.Services
{
    public class GetByServices : IGetByServices
    {
        private readonly IUnitOfWork _unitOfWork1;
        public GetByServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork1 = unitOfWork;
        }

        public IEnumerable<Pizza> GetByIngredient(string Ingredient)
        {
            return _unitOfWork1._getByRepository.GetByIngredient(Ingredient);
        }

        public IEnumerable<Pizza> GetByPreparationTime(int PreparationTime)
        {
            return _unitOfWork1._getByRepository.GetByPreparationTime(PreparationTime);
        }

        public IEnumerable<Pizza> GetByRating(int Rating)
        {
            return _unitOfWork1._getByRepository.GetByRating(Rating);
        }

        public IEnumerable<Pizza> GetMostPopular()
        {
            return _unitOfWork1._getByRepository.GetMostPopular();
        }

        public IEnumerable<Pizza> GetnutritionalInformation()
        {
            return _unitOfWork1._getByRepository.GetnutritionalInformation();
        }

        public IEnumerable<Pizza> GetnutritionalInformationById(int id)
        {
            return _unitOfWork1._getByRepository.GetnutritionalInformationById((int)id);
        }

        public IEnumerable<Pizza> GetnutritionalInformationByName(string name)
        {
            return _unitOfWork1._getByRepository.GetnutritionalInformationByName(name);
        }

        public IEnumerable<Pizza> GetnutritionalInformationByNameOfAllPizza(string name)
        {
            return _unitOfWork1._getByRepository.GetnutritionalInformationByNameOfAllPizza(name);
        }

        public IEnumerable<Pizza> GetAMenuOfPizza()
        {
            return _unitOfWork1._getByRepository.GetAMenuOfPizza();
        }

        public object GetIngredientsByPizzaName(string name)
        {
            return _unitOfWork1._getByRepository.GetIngredientsByPizzaName((string)name);
        }
        
        public void ValorateAPizza(int NewRating, Pizza pizza)
        {
            _unitOfWork1._getByRepository.ValorateAPizza(NewRating, pizza);
            _unitOfWork1.SaveChanges();
        }

        public void NewCommentPizza(string NewComment, Pizza pizza)
        {
            _unitOfWork1._getByRepository.NewCommentPizza(NewComment, pizza);
            _unitOfWork1.SaveChanges();
        }
    }
}
