using AplicationDomainLayer___PizzaDay.Entities;
using AplicationDomainLayer___PizzaDay.Exceptions;
using AplicationDomainLayer___PizzaDay.Interfaces;
using InfrastructureLayer___PizzaDay.Data;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace InfrastructureLayer___PizzaDay.Repositories
{
    public class GetByRepository : IGetByRepository
    {
        private readonly PIZZAContext _contextPizza;
        private readonly DbSet<Pizza> _dbSet;
        public GetByRepository(PIZZAContext contextPizza)
        {
            _contextPizza = contextPizza;
            _dbSet = contextPizza.Set<Pizza>();
        }

        public IEnumerable<Pizza> GetByPreparationTime(int PreparationTime)
        {
            int? MaxValue = _dbSet.Max(x => x.PreparationTime);
            
            PreparationsTimeValidations(PreparationTime, MaxValue);

            return _dbSet.Where(x => x.PreparationTime == PreparationTime).ToList();
        }

        public void PreparationsTimeValidations(int PreparationsTime, int? MaxValue)
        {
            if (PreparationsTime < 12) 
            {
                throw new GlobalBusinessExceptions("The Preparation Time of a pizza shouldn't be less than 12 minutes, please enter a number bigger than 12 minutes");
            }

            if (PreparationsTime > MaxValue) 
            {
                throw new GlobalBusinessExceptions($"The Max time that a pizza should cook is {MaxValue} Minutes, More than it, and you will eat coal");
            }
        }

        public IEnumerable<Pizza> GetByRating(int Rating)
        {
            GetByRatingValidation(Rating);
            return _dbSet.Where(x => x.Rating == Rating).ToList();
        }

        public void GetByRatingValidation(int Rating)
        {
            if (Rating == null)
            {
                throw new GlobalBusinessExceptions("The Rating should be less than 10, thats the top");
            }
        }

        public IEnumerable<Pizza> GetByIngredient(string Ingredient)
        {
            GetByIngredientValidation(Ingredient);
            return _dbSet.Where(x => x.Ingredients.Contains(Ingredient)).ToList();
        }

        public void GetByIngredientValidation(string Ingredient)
        {
            if (Ingredient == null)
            {
                throw new GlobalBusinessExceptions($"mmm... probably a pizza with the ingredient {Ingredient} is not in our Menu");
            }
        }

        public IEnumerable<Pizza> GetMostPopular()
        {
            var MaxRating = _dbSet.Max(x => x.Rating);

            List<Pizza> Pizzas = _dbSet.Where(x => x.Rating == MaxRating).Take(3).ToList();

            if(Pizzas.Count < 3)
            {
                var EntitySecondRatingV2 = _dbSet.OrderByDescending(x => x.Rating).Skip(2).FirstOrDefault();
                Pizzas.Add(EntitySecondRatingV2);
                return Pizzas;
            }

            return Pizzas;
        }

        public IEnumerable<Pizza> GetnutritionalInformation()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<Pizza> GetnutritionalInformationById(int id)
        {
            return _dbSet.Where(x => x.Id == id).ToList();
        }

        public IEnumerable<Pizza> GetnutritionalInformationByName(string name)
        {
            var pizza = _dbSet?.Where(x => x.PizzaName.ToLower() == name.ToLower()).ToList();

            if (pizza.Count() == 0)
            {
                var Pizza = _dbSet?.ToList().FirstOrDefault(x => x.PizzaName.Contains(name, StringComparison.OrdinalIgnoreCase));

                List<Pizza> BoxPizza = new List<Pizza>();
                BoxPizza.Add(Pizza);

                return BoxPizza;
            }
            return pizza;
        }

        public IEnumerable<Pizza> GetnutritionalInformationByNameOfAllPizza(string name)
        {
            var pizza = _dbSet?.Where(x => x.PizzaName.ToLower() == name.ToLower()).ToList();

            if (pizza.Count() == 0)
            {
                return _dbSet?.ToList().Where(x => x.PizzaName.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            return pizza;
        }

        public IEnumerable<Pizza> GetAMenuOfPizza()
        {
            var pizzas = _dbSet.ToList();

            if (pizzas != null)
            {
                List<Pizza> PizzaBox = new List<Pizza>();

                for (int i = 0; i < 5;  i++) 
                {

                    Random IdRamdon = new Random();
                    var pizzasFive = pizzas[IdRamdon.Next(0, pizzas.Count())];

                    PizzaBox.Add(pizzasFive);

                }
                return PizzaBox;

            }

            return pizzas;
        }

        public object GetIngredientsByPizzaName(string name)
        {
            var pizza = _dbSet?.FirstOrDefault(x => x.PizzaName.ToLower() == name.ToLower());

            if(pizza == null) 
            {
                return ValidationPizzaIngredientsName(pizza, name);
            }

            string? Ingredients = pizza.Ingredients;
            string? NamePizza = pizza.PizzaName;

            var AllInfomation = new
            {
                NameOfThePizza = NamePizza,
                PizzaIngredients = Ingredients
            };

            return AllInfomation;
        }

        public object ValidationPizzaIngredientsName(Pizza pizza, string name)
        {
            var pizzas = _dbSet?.ToList().FirstOrDefault(x => x.PizzaName.Contains(name, StringComparison.OrdinalIgnoreCase));

            string? ingredients = pizzas.Ingredients;
            string? NamePizzas = pizzas.PizzaName;

            var AllInfomations = new
            {
                NameOfThePizza = NamePizzas,
                PizzaIngredients = ingredients
            };

            return AllInfomations;
        }

        public void ValorateAPizza(int NewRating, Pizza pizza)
        {
            int? OldRating = pizza.Rating;
            pizza.Rating = (NewRating + OldRating) / 2;

            _dbSet.Update(pizza);
        }

        public void NewCommentPizza(string NewComment, Pizza pizza)
        {
            string? OldComment = pizza.Comments;
            pizza.Comments = $"{OldComment}, {NewComment}";

            _dbSet.Update(pizza);
        }
    }
}
