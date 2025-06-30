using DAL;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ForTest
{
    public class RepositoryTest
    {
        private static List<Car> GetFakeCars() => new()
        {
            new Car
            {
                Id = "1",
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2020,
                FuelType = "Petrol",
                Transmission = "Manual",
                Seats = 5,
                Color = "Red",
                DailyRate = 50,
                HourlyRate = 10,
                IsAvailable = true,
                LicensePlate = "TEST-001",
                Vin = "VIN11111111111111",
                Location = "Garage A",
                Features = new List<string> { "Bluetooth" },
                Images = new List<string> { "img1.jpg" }
            },
            new Car
            {
                Id = "2",
                Brand = "Ford",
                Model = "Focus",
                Year = 2019,
                FuelType = "Diesel",
                Transmission = "Auto",
                Seats = 7,
                Color = "Blue",
                DailyRate = 60,
                HourlyRate = 12,
                IsAvailable = true,
                LicensePlate = "TEST-002",
                Vin = "VIN22222222222222",
                Location = "Garage B",
                Features = new List<string> { "CruiseControl" },
                Images = new List<string> { "img2.jpg" }
            }
        };



        private static CarsRepository GetRepositoryWithInMemoryDb(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new ApplicationContext(options);
            context.Cars.AddRange(GetFakeCars());
            context.SaveChanges();

            return new CarsRepository(context);
        }

        [Fact]
        public async Task GetAllCarsByCharacteristicAsync_ReturnsCorrectCars()
        {
            var repo = GetRepositoryWithInMemoryDb(nameof(GetAllCarsByCharacteristicAsync_ReturnsCorrectCars));

            var result = await repo.GetAllCarsByCharacteristicAsync("Petrol", "Manual", 5, "Red");

            Assert.Single(result);
            var car = result.First();
            Assert.Equal("Petrol", car.FuelType);
            Assert.Equal("Manual", car.Transmission);
            Assert.Equal("Red", car.Color);
            Assert.True(car.Seats <= 5);
        }

        [Fact]
        public async Task GetAllCarsFilteredAsync_ReturnsAvailableByBrand()
        {
            var repo = GetRepositoryWithInMemoryDb(nameof(GetAllCarsFilteredAsync_ReturnsAvailableByBrand));

            var result = await repo.GetAllCarsFilteredAsync("Ford", null, null, null);

            Assert.Single(result);
            Assert.Equal("Ford", result.First().Brand);
            Assert.True(result.First().IsAvailable);
        }
    }
}
