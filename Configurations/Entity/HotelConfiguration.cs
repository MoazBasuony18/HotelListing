using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Data;

namespace WebApplication1.Configurations.Entity
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Egypt Star Hotel",
                    Address = "Cairo",
                    Rating = 4.6,
                    CountryId = 1,
                },
                 new Hotel
                 {
                     Id = 4,
                     Name = "Egypt Star Hotel 2",
                     Address = "Zamalek",
                     Rating = 4.5,
                     CountryId = 1,
                 },
                new Hotel
                {
                    Id = 2,
                    Name = "Alexandria Star",
                    Address = "Alex",
                    Rating = 4.5,
                    CountryId = 2,
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Hurghada Hotel",
                    Address = "Sinai",
                    Rating = 4.8,
                    CountryId = 3,
                });
        }
    }
}
