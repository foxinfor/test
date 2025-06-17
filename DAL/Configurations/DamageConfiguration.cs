using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class DamageConfiguration : IEntityTypeConfiguration<Damage>
    {
        public void Configure(EntityTypeBuilder<Damage> builder)
        {
            builder.Property(d => d.Description).HasMaxLength(255).IsRequired();
            builder.Property(d => d.Severity).HasMaxLength(50).IsRequired();

            builder.Property(d => d.RepairCost).IsRequired();
            builder.HasCheckConstraint("CK_Damage_RepairCost", "RepairCost > 0");
        }
    }
}
