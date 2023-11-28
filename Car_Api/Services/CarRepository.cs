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
        public async Task<(IEnumerable<Brand>, PaginationMetadata)> GetAllBrandsAsync(string? name
            , string? searchQuery, int pageNumber, int pageSize)
        {
            var collection = _context.Brands as IQueryable<Brand>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(b => b.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection
                    .Where(b => b.Name.Contains(searchQuery)
                                ||(b.Description!=null
                                   &&b.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetaData = 
                new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn =  await collection
                .OrderBy(b => b.Name)
                .Skip(pageSize*(pageNumber-1))
                .Take(pageSize)
                .ToListAsync();
            return (collectionToReturn, paginationMetaData);
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
