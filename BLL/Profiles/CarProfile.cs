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
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<CreateCarDTO, Car>().ReverseMap();
            CreateMap<UpdateCarDTO, Car>().ReverseMap();
        }
    }
}
