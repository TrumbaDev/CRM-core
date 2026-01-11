using CrmCore.Infrastructure.Data.User.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrmCore.Infrastructure.Data.User.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.MiddleName).HasMaxLength(100);

        builder.Property(x => x.Email).HasMaxLength(255);
        builder.Property(x => x.Phone).HasMaxLength(15);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedNever();
        
        builder.Property(x => x.UpdatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedNever();
    }
}