using System.Net;
using System.Text.Json;
using AutoMapper;
using Car_Api.Models;
using Car_Api.Models.Dtos;
using Car_Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/brands")]
    public class BrandsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly ApiResponse _response;
        private const int MaxBrandPageSize = 20;

        public BrandsController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            this._response = new ApiResponse() ;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandWithoutCarsDto>>> GetBrandsAsync
            (string? name, string? searchQuery, int pageNumber=1, int pageSize=10 )
        {
            if (pageSize > MaxBrandPageSize)
            {
                pageSize = MaxBrandPageSize;
            }
            var (brandsFromBd,paginationMetadata) = await _carRepository.GetAllBrandsAsync
                (name, searchQuery,pageNumber,pageSize);
            
            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(paginationMetadata));
            
            return Ok(_mapper.Map<IEnumerable<BrandWithoutCarsDto>>(brandsFromBd));

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
