using AplicationDomainLayer___PizzaDay.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureLayer___PizzaDay.Configurations
{
    public class PizzaConfigurations : IEntityTypeConfiguration<Pizza>
    {
        public void Configure(EntityTypeBuilder<Pizza> builder)
        {
            builder.ToTable("Pizza");

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.Calories).HasColumnName("Calories");
            builder .Property(e => e.Fats).HasColumnName("Fats");
            builder.Property(e => e.Carbohydrates).HasColumnName("Carbohydrates");
            builder.Property(e => e.Proteins).HasColumnName("Proteins");
            builder.Property(e => e.PreparationTime).HasColumnName("PreparationTime");
            builder .Property(e => e.Rating).HasColumnName("Rating");

            builder.Property(e => e.Author).HasColumnName("Author")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Category).HasColumnName("Category")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Comments).HasColumnName("Comments")
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.DietaryRestrictions).HasColumnName("DietaryRestrictions")
                .HasMaxLength(1500)
                .IsUnicode(false);

            builder.Property(e => e.ImgUrl).HasColumnName("ImgUrl")
                .HasMaxLength(2500)
                .IsUnicode(false);

            builder.Property(e => e.Ingredients).HasColumnName("Ingredients")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.PizzaName).HasColumnName("PizzaName")
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.Preparation).HasColumnName("Preparation")
                .HasMaxLength(5000)
                .IsUnicode(false);
        }
    }
}
