using AutoMapper;
using Car_Api.Models;
using Car_Api.Models.Dtos;

namespace Car_Api.Profiles
{
    public class CarProfiles : Profile
    {
        public CarProfiles()
        {
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<Car, CarForUpdateDto>().ReverseMap();
            CreateMap<Car, CarForCreationDto>().ReverseMap();
        }
    }
}
