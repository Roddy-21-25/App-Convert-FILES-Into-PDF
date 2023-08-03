using System.ComponentModel.DataAnnotations;

namespace AplicationDomainLayer___PizzaDay.DTOs
{
    public class PrescriptionPizzaDTO
    {
        [Required]
        public string? PizzaName { get; set; }
        [Required]
        public string? ImgUrl { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public int? Calories { get; set; }
        [Required]
        public int? Fats { get; set; }
        [Required]
        public int? Carbohydrates { get; set; }
        [Required]
        public int? Proteins { get; set; }
        [Required]
        public string? Ingredients { get; set; }
        [Required]
        public string? Preparation { get; set; }
        [Required]
        public int? PreparationTime { get; set; }
        [Required]
        public string? DietaryRestrictions { get; set; }
        [Required]
        public int? Rating { get; set; }
        [Required]
        public string? Comments { get; set; }
    }
}
