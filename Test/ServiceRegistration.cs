using BLL.DTO.Models;
using BLL.DTO.Requests;
using BLL.Interfaces;
using BLL.Services;
using BLL.Validators;
using DAL.Interfaces;
using DAL.Repositories;
using FluentValidation;

namespace Test
{
    public static class ServiceRegistration
    {

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ICarRepository, CarsRepository>();
            services.AddScoped<IInspectionReportRepository, InspectionReportRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IInspectionReportService, InspectionReportService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IPdfService, PdfService>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<BookingDTO>, BookingDTOValidator>();
            services.AddScoped<IValidator<CreateBookingDTO>, CreateBookingDTOValidator>();
            services.AddScoped<IValidator<UpdateBookingDTO>, UpdateBookingDTOValidator>();

            services.AddScoped<IValidator<CarDTO>, CarDTOValidator>();
            services.AddScoped<IValidator<CreateCarDTO>, CreateCarDTOValidator>();
            services.AddScoped<IValidator<UpdateCarDTO>, UpdateCarDTOValidator>();


            services.AddScoped<IValidator<InspectionReportDTO>, InspectionReportDTOValidator>();
            services.AddScoped<IValidator<CreateInspectionReportDTO>, CreateInspectionReportDTOValidator>();
            services.AddScoped<IValidator<UpdateInspectionReportDTO>, UpdateInspectionReportDTOValidator>();

        }
    }
}
