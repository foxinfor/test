using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);


            builder.Property(b => b.Status).HasMaxLength(20).IsRequired();
            builder.Property(b => b.PaymentStatus).HasMaxLength(20).IsRequired();
            builder.Property(b => b.InsuranceType).HasMaxLength(50);
            builder.Property(b => b.PickupLocation).HasMaxLength(100);
            builder.Property(b => b.DropoffLocation).HasMaxLength(100);

            builder.Property(b => b.StartDate).IsRequired();
            builder.Property(b => b.EndDate).IsRequired();

            builder.Property(b => b.TotalPrice).IsRequired();
            builder.HasCheckConstraint("CK_Booking_TotalPrice", "TotalPrice > 0");
        }
    }
}
