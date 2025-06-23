using BLL.DTO.Models;
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
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IImageService, ImageService>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<BookingDTO>, BookingDTOValidator>();
            
        }

    }
}
