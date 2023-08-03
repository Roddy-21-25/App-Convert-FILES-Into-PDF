namespace AplicationDomainLayer___PizzaDay.Entities
{
    public partial class Pizza : BaseIdEntity
    {
        public string? PizzaName { get; set; }
        public string? ImgUrl { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }
        public int? Calories { get; set; }
        public int? Fats { get; set; }
        public int? Carbohydrates { get; set; }
        public int? Proteins { get; set; }
        public string? Ingredients { get; set; }
        public string? Preparation { get; set; }
        public int? PreparationTime { get; set; }
        public string? DietaryRestrictions { get; set; }
        public int? Rating { get; set; }
        public string? Comments { get; set; }
    }
}
