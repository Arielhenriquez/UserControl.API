using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserControl.Model.Entities;

namespace UserControl.Model.Configurations;

public class PhoneEntityConfiguration : IEntityTypeConfiguration<PhoneEntity>
{
    public void Configure(EntityTypeBuilder<PhoneEntity> builder)
    {
        builder.Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(12); 

        builder.Property(p => p.CityCode)
            .IsRequired()
            .HasMaxLength(5); 

        builder.Property(p => p.CountryCode)
            .IsRequired()
            .HasMaxLength(3);  

        builder.HasOne(p => p.UserEntity)
            .WithMany(u => u.Phones)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
