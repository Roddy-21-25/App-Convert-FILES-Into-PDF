using AplicationDomainLayer___PizzaDay.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureLayer___PizzaDay.Configurations
{
    public class ChefConfigurations : IEntityTypeConfiguration<Chef>
    {
        public void Configure(EntityTypeBuilder<Chef> builder)
        {
            builder.ToTable("Chef");

            builder.Property(e => e.Id).HasColumnName("Id");

            builder.Property(e => e.UserName)
                .HasColumnName("UserName")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.UserPassword)
                .HasColumnName("UserPassword")
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
