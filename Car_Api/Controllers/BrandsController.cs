using System.Net;
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
        private readonly ApiResponse _response;

        public BrandsController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            this._response = new ApiResponse() ;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetBrandsAsync()
        {
            var brandsFromBd = await _carRepository.GetAllBrandsAsync();
            _response.Result = _mapper.Map<IEnumerable<BrandWithoutCarsDto>>(brandsFromBd);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetBrand(int id, bool includeCars = false)
        {
            var brandFromDb = await _carRepository.GetBrandAsync(id, includeCars);
            if (brandFromDb == null)
            {
                return NotFound();
            }
            if (includeCars)
            {
                _response.Result = _mapper.Map<BrandDto>(brandFromDb);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }

            _response.Result = _mapper.Map<BrandWithoutCarsDto>(brandFromDb);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
    }
}
