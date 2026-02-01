using CrmCore.Infrastructure.Data.SalesFunnel.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmCore.Infrastructure.Data.SalesFunnel;

public class SalesFunnelDbContext : DbContext
{
    public DbSet<SalesFunnelModel> SalesFunnel => Set<SalesFunnelModel>();
    public DbSet<FunnelStageModel> FunnelStage => Set<FunnelStageModel>();

    public SalesFunnelDbContext(DbContextOptions<SalesFunnelDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesFunnelDbContext).Assembly);
    }
}