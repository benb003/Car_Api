using System.Net;
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
        private readonly ApiResponse _response;

        public CarsController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            this._response = new ApiResponse();
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetCars(int brandId)
        {
            if (!await _carRepository.BrandExistAsync(brandId))
            {
                return NotFound();
            }

            var carsFromDb = await _carRepository.GetAllCarsForBrandAsync(brandId);
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<IEnumerable<CarDto>>(carsFromDb); 
            return Ok(_response);
        }

        [HttpGet("{carId:int}", Name = "GetCar")]
        public async Task<ActionResult<ApiResponse>> GetCar(int brandId, int carId)
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
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<CarDto>(carFromDb); 
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateCar(int brandId, CarForCreationDto carForCreationDto)
        {
            if (!await _carRepository.BrandExistAsync(brandId))
            {
                return NotFound();
            }

            var carToAddToDb = _mapper.Map<Car>(carForCreationDto);

            await _carRepository.AddCar(brandId, carToAddToDb);
            await _carRepository.SaveChangesAsync();
            
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<CarDto>(carToAddToDb);
            return CreatedAtRoute("GetCar", new { brandId = carToAddToDb.BrandId, carId = carToAddToDb.Id },
                _response);
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

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(carToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(carToPatch, carFromDb);

            await _carRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}