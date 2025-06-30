using AutoMapper;
using BLL.DTO.Models;
using BLL.DTO.Requests;
using DAL.Models;

namespace BLL.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile() 
        {
            CreateMap<Car, CarDTO>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Vin, opt => opt.MapFrom(src => src.Vin));
            CreateMap<CreateCarDTO, Car>().ReverseMap();
            CreateMap<UpdateCarDTO, Car>().ReverseMap();
        }
    }
}
