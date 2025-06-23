using AutoMapper;
using BLL.DTO.Models;
using BLL.DTO.Requests;
using DAL.Models;

namespace BLL.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile() 
        {
            CreateMap<Booking,BookingDTO>().ReverseMap();
            CreateMap<CreateBookingDTO,Booking>().ReverseMap();
            CreateMap<UpdateBookingDTO, Booking>().ReverseMap();
        }
    }
}
