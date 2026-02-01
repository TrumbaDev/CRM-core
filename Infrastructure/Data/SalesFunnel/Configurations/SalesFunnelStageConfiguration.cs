using CrmCore.Infrastructure.Data.SalesFunnel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrmCore.Infrastructure.Data.SalesFunnel.Configurations;

public class SalesFunnelStageConfiguration
{
    public void Configure(EntityTypeBuilder<FunnelStageModel> builder)
    {
        builder.ToTable("sales_funnels_stages");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityAlwaysColumn();
        
        builder.HasIndex(x => new {x.FunnelId});

        builder.Property(x => x.Name)
            .HasMaxLength(255);

        builder.Property(x => x.IndexNumber)
            .HasMaxLength(2);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("timestamp")
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAddOrUpdate();
    }
}