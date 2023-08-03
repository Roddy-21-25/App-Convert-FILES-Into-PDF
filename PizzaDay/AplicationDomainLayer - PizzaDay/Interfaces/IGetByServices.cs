using AplicationDomainLayer___PizzaDay.Entities;

namespace AplicationDomainLayer___PizzaDay.Interfaces
{
    public interface IGetByServices
    {
        IEnumerable<Pizza> GetByPreparationTime(int PreparationTime);
        IEnumerable<Pizza> GetByRating(int Rating);
        IEnumerable<Pizza> GetByIngredient(string Ingredient);

        IEnumerable<Pizza> GetMostPopular();
        IEnumerable<Pizza> GetnutritionalInformation();

        IEnumerable<Pizza> GetnutritionalInformationById(int id);
        IEnumerable<Pizza> GetnutritionalInformationByName(string name);
        IEnumerable<Pizza> GetnutritionalInformationByNameOfAllPizza(string name);
        IEnumerable<Pizza> GetAMenuOfPizza();
        object GetIngredientsByPizzaName(string name);
        void ValorateAPizza(int NewRating, Pizza pizza);
        void NewCommentPizza(string NewComment, Pizza pizza);
    }
}