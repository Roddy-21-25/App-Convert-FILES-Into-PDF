using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDomainLayer___PizzaDay.DTOs
{
    public class PizzaNutritionalInformationDTO
    {
        public string? PizzaName { get; set; }
        public string? ImgUrl { get; set; }

        public string? Category { get; set; }
        public int? Calories { get; set; }
        public int? Fats { get; set; }
        public int? Carbohydrates { get; set; }
        public int? Proteins { get; set; }
        public string? Ingredients { get; set; }
        public string? DietaryRestrictions { get; set; }
    }
}
