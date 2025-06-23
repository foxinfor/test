using AutoMapper;
using BLL.DTO.Models;
using BLL.DTO.Requests;
using DAL.Models;

namespace BLL.Profiles
{
    public class InspectionReportProfile : Profile
    {
        public InspectionReportProfile() 
        {
            CreateMap<InspectionReport, InspectionReportDTO>().ReverseMap();
            CreateMap<CreateInspectionReportDTO, InspectionReport>().ReverseMap();
            CreateMap<UpdateInspectionReportDTO, InspectionReport>().ReverseMap();
        }
    }
}
