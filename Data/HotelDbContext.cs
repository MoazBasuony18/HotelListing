using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class HotelDbContext:IdentityDbContext<ApiUser>
    {
        public HotelDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasData(
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
            modelBuilder.Entity<Hotel>().HasData(
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
