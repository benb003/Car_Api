using AutoMapper;
using Car_Api.Models;
using Car_Api.Models.Dtos;

namespace Car_Api.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandWithoutCarsDto>().ReverseMap();
            CreateMap<Brand, BrandDto>().ReverseMap();
        }
    }
}
