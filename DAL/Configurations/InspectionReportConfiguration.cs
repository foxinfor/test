using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class InspectionReportConfiguration : IEntityTypeConfiguration<InspectionReport>
    {
        public void Configure(EntityTypeBuilder<InspectionReport> builder)
        {
            builder.HasKey(ir => ir.Id);

            builder.Property(ir => ir.Notes).HasMaxLength(500);

            builder.Property(ir => ir.ReturnDate).IsRequired();

            builder.Property(ir => ir.FuelLevel).IsRequired();

            builder.Property(ir => ir.Mileage).IsRequired();
            builder.HasCheckConstraint("CK_InspectionReport_Mileage", "Mileage >= 0");

            builder.Property(ir => ir.FinalCharge).IsRequired();
            builder.HasCheckConstraint("CK_InspectionReport_FinalCharge", "FinalCharge >= 0");


        }
    }
}
