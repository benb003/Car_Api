using Car_Api.Data;
using Car_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Api.Services
{
    public class CarRepository : ICarRepository
    {
        private readonly Context _context;

        public CarRepository(Context context)
        {
            _context = context;
        }

        public async Task AddCar(int brandId, Car car)
        {
            var brand = await GetBrandAsync(brandId, false);

            if (brand != null)
            {
                brand.Cars.Add(car);
            }
        }

        public async Task<bool> BrandExistAsync(int brandId)
        {
            return await _context.Brands.AnyAsync(b => b.Id == brandId);
        }

        public void DeleteCar(Car car)
        {
            _context.Cars.Remove(car);
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brands.OrderBy(b => b.Name).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetAllCarsForBrandAsync(int brandId)
        {
            return await _context.Cars.Where(c => c.BrandId == brandId).ToListAsync();
        }

        public async Task<Brand?> GetBrandAsync(int brandId, bool includeCars)
        {
            if (includeCars)
            {
                await _context.Brands.Include(b => b.Cars).Where(b => b.Id == brandId).FirstOrDefaultAsync();
            }
            return await _context.Brands.Where(b => b.Id == brandId).FirstOrDefaultAsync();
        }

        public async Task<Car?> GetCarForBrandAsync(int brandId, int carId)
        {
            return await _context.Cars
               .Where(p => p.BrandId == brandId && p.Id == carId)
               .FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
