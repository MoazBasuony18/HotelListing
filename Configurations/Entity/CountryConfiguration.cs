using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data;

namespace WebApplication1.Configurations.Entity
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                 new Country
                 {
                     Id = 1,
                     Name = "Egypt",
                     ShortName = "EG"
                 },
                new Country
                {
                    Id = 2,
                    Name = "Alexandria",
                    ShortName = "Alex"
                },
                new Country
                {
                    Id = 3,
                    Name = "Hurghada",
                    ShortName = "HG"
                });
        }
    }
}
