using AutoMapper;
using Car_Api.Models;
using Car_Api.Models.Dtos;
using Car_Api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Car_Api.Controllers
{
    [ApiController]
    [Route("/api/brands/{brandId}/cars")]
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarsController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CarDto>> GetCars(int brandId)
        {
            if (!await _carRepository.BrandExistAsync(brandId))
            {
                return NotFound();
            }
            var carsFromDb = await _carRepository.GetAllCarsForBrandAsync(brandId);

            return Ok(_mapper.Map<IEnumerable<CarDto>>(carsFromDb));
        }

        [HttpGet("{carId}", Name = "GetCar")]
        public async Task<ActionResult<Car>> GetCar(int brandId, int carId)
        {
            if (!await _carRepository.BrandExistAsync(brandId))
            {
                return NotFound();
            }
            var carFromDb = await _carRepository.GetCarForBrandAsync(brandId, carId);
            if (carFromDb == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CarDto>(carFromDb));
        }

        [HttpPost]
        public async Task<ActionResult<CarDto>> CreateCar(int brandId, CarForCreationDto carForCreationDto)
        {
            if (!await _carRepository.BrandExistAsync(brandId))
            {
                return NotFound();
            }

            var carToAddToDb = _mapper.Map<Car>(carForCreationDto);

            await _carRepository.AddCar(brandId, carToAddToDb);
            await _carRepository.SaveChangesAsync();

            return CreatedAtRoute("GetCar",new{brandId = carToAddToDb.BrandId, carId=carToAddToDb.Id},carToAddToDb);
        }

        [HttpPut("{carId}")]
        public async Task<ActionResult> UpdateCar(int brandId, int carId, CarForUpdateDto carForUpdateDto)
        {
            if (!await _carRepository.BrandExistAsync(brandId))
            {
                return NotFound();
            }
            var carFromDb = await _carRepository.GetCarForBrandAsync(brandId, carId);
            if (carFromDb == null)
            {
                return NotFound();
            }
            _mapper.Map(carForUpdateDto, carFromDb);
            await _carRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCar(int brandId, int carId)
        {
            if (!await _carRepository.BrandExistAsync(brandId))
            {
                return NotFound();
            }
            var carFromDb = await _carRepository.GetCarForBrandAsync(brandId, carId);
            if (carFromDb == null)
            {
                return NotFound();
            }
            _carRepository.DeleteCar(carFromDb);

            await _carRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{carId}")]
        public async Task<ActionResult> PartiallyUpdateCar(int brandId, int carId, 
            JsonPatchDocument<CarForUpdateDto> patchDocument)
        {
            if (!await _carRepository.BrandExistAsync(brandId))
            {
                return NotFound();
            }
            var carFromDb = await _carRepository.GetCarForBrandAsync(brandId, carId);
            if (carFromDb == null)
            {
                return NotFound();
            }

            var carToPatch = _mapper.Map<CarForUpdateDto>(carFromDb);

            patchDocument.ApplyTo(carToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(!TryValidateModel(carToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(carToPatch, carFromDb);

            await _carRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
