using AplicationDomainLayer___PizzaDay.Entities;
using InfrastructureLayer___PizzaDay.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer___PizzaDay.Data
{
    public partial class PIZZAContext : DbContext
    {
        public PIZZAContext()
        {
        }

        public PIZZAContext(DbContextOptions<PIZZAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chef> Chefs { get; set; } = null!;
        public virtual DbSet<Pizza> Pizzas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChefConfigurations());
            modelBuilder.ApplyConfiguration(new PizzaConfigurations());
        }
    }
}
