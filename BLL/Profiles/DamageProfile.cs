using AutoMapper;
using BLL.DTO.Models;
using BLL.DTO.Requests;
using DAL.Models;

namespace BLL.Profiles
{
    public class DamageProfile : Profile
    {
        public DamageProfile()
        {
            CreateMap<Damage, DamageDTO>().ReverseMap();
            CreateMap<CreateDamageDTO, Damage>().ReverseMap();
            CreateMap<UpdateDamageDTO, Damage>().ReverseMap();
        }
    }
}
