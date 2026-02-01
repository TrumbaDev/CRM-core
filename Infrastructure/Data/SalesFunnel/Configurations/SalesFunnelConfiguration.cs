using CrmCore.Infrastructure.Data.SalesFunnel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrmCore.Infrastructure.Data.SalesFunnel.Configurations;

public class SalesFunnelConfiguration
{
    public void Configure(EntityTypeBuilder<SalesFunnelModel> builder)
    {
        builder.ToTable("sales_funnels");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.Name)
            .HasMaxLength(255);

        builder.HasMany(x => x.Stages)
            .WithOne()
            .HasForeignKey(x => x.FunnelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.CreateAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAddOrUpdate();
    }
}