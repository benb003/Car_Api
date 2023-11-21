using AutoMapper;
using Car_Api.Models;
using Car_Api.Models.Dtos;
using Car_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Car_Api.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public BrandsController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrandsAsync()
        {
            var brandsFromBd = await _carRepository.GetAllBrandsAsync();
            return Ok(_mapper.Map<IEnumerable<BrandWithoutCarsDto>>(brandsFromBd));

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBrand(int id, bool includeCars = false)
        {
            var brandFromDb = await _carRepository.GetBrandAsync(id, includeCars);
            if (brandFromDb == null)
            {
                return NotFound();
            }
            if (includeCars)
            {
                return Ok(brandFromDb);
            }
            return Ok(_mapper.Map<BrandWithoutCarsDto>(brandFromDb));
        }
    }
}
