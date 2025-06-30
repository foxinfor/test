using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Brand).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Model).HasMaxLength(50).IsRequired();
            builder.Property(c => c.LicensePlate).HasMaxLength(15).IsRequired();
            builder.Property(c => c.Vin).HasMaxLength(17).IsRequired();
            builder.Property(c => c.FuelType).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Transmission).HasMaxLength(20).IsRequired();
            builder.Property(c => c.Color).HasMaxLength(20);

            builder.HasCheckConstraint("CK_Car_DailyRate", "DailyRate > 0");
            builder.HasCheckConstraint("CK_Car_HourlyRate", "HourlyRate > 0");
            builder.HasCheckConstraint("CK_Car_Seats", "Seats > 0");
            builder.HasCheckConstraint("CK_Car_Year", "Year >= 1886");


            builder.Property(c => c.Location)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
