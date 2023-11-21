using Car_Api.Models;

namespace Car_Api.Services
{
    public interface ICarRepository
    {
        Task<IEnumerable<Brand>> GetAllBrandsAsync();
        Task<Brand?> GetBrandAsync(int brandId, bool includeCars);
        Task<bool> BrandExistAsync(int brandId);
        Task<IEnumerable<Car>> GetAllCarsForBrandAsync(int brandId);
        Task<Car?> GetCarForBrandAsync(int brandId, int carId);
        Task AddCar(int brandId, Car car);
        void DeleteCar(Car car);
        Task<bool> SaveChangesAsync();

    }
}
