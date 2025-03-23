using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserControl.Model.Entities;

namespace UserControl.Model.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(u => u.Password)
           .IsRequired()
           .HasMaxLength(30);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(u => u.LastLogin)
            .HasColumnType("timestamptz");

        builder.Property(u => u.IsActive)
            .IsRequired();

        builder.HasMany(u => u.Phones)
            .WithOne(p => p.UserEntity)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
