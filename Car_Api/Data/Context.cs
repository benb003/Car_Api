using Car_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Api.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(
                new Brand()
                {
                    Id = 1,
                    Name = "Volvo",
                    Country = "Sweden",
                    Description = "v60 is cool car",
                }, new Brand()
                {
                    Id = 2,
                    Name = "Bmw",
                    Country = "Germany",
                    Description = "530e is nice car",
                }, new Brand()
                {
                    Id = 3,
                    Name = "Tesla",
                    Country = "USA",
                    Description = "i have no idea about this one",
                }, new Brand()
                {
                    Id = 4,
                    Name = "Toyoto",
                    Country = "Japan",
                    Description = "Reliable car, they say",
                }, new Brand()
                {
                    Id = 5,
                    Name = "Kia",
                    Country = "South Korea",
                    Description = "my favorite xceed",
                });

            modelBuilder.Entity<Car>().HasData(
                new Car()
                {
                    Id = 1,
                    Model = "V60",
                    Year = 2021,
                    IsElectric = false,
                    Description = "Car of the year",
                    BrandId = 1
                }, new Car()
                {
                    Id = 2,
                    Model = "530e",
                    Year = 2022,
                    IsElectric = false,
                    Description = "cool",
                    BrandId = 2
                }, new Car()
                {
                    Id = 3,
                    Model = "Model X",
                    Year = 2021,
                    IsElectric = true,
                    Description = "car car car",
                    BrandId = 3
                }, new Car()
                {
                    Id = 4,
                    Model = "Model 3",
                    Year = 2023,
                    IsElectric = true,
                    Description = "maybe maybe",
                    BrandId = 3
                }, new Car()
                {
                    Id = 5,
                    Model = "Corolla Hybrid",
                    Year = 2020,
                    IsElectric = false,
                    Description = "good one",
                    BrandId = 4
                }, new Car()
                {
                    Id = 6,
                    Model = "xCeed",
                    Year = 2023,
                    IsElectric = true,
                    Description = "crossover like car",
                    BrandId = 5
                }, new Car()
                {
                    Id = 7,
                    Model = "Sorento",
                    Year = 2011,
                    IsElectric = false,
                    Description = "tractor",
                    BrandId = 5
                }, new Car()
                {
                    Id = 8,
                    Model = "Picanto",
                    Year = 2019,
                    IsElectric = false,
                    Description = "not a car bicycle",
                    BrandId = 5
                }
                );


            base.OnModelCreating(modelBuilder);
        }
    }
}
