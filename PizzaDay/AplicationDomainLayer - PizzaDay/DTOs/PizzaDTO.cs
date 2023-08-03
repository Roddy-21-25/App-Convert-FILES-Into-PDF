using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDomainLayer___PizzaDay.DTOs
{
    public class PizzaDTO
    {
        public string? PizzaName { get; set; }
        public string? ImgUrl { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public string? Ingredients { get; set; }
        public string? Preparation { get; set; }
        public int? PreparationTime { get; set; }
        public string? DietaryRestrictions { get; set; }
        public int? Rating { get; set; }
        public string? Comments { get; set; }
    }
}
