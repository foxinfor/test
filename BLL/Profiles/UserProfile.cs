using AutoMapper;
using BLL.DTO.Requests;
using BLL.DTO.Responces;
using DAL.Models;

namespace BLL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterDTO, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(dest => dest.Id, opt => opt.Ignore()); ;

            CreateMap<User, AuthResponseDTO>()
               .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken))
               .ForMember(dest => dest.IsAuthenticated, opt => opt.MapFrom(src => true))
               .ForMember(dest => dest.ErrorMessage, opt => opt.Ignore())
               .ForMember(dest => dest.Token, opt => opt.Ignore());
        }
    }
}
