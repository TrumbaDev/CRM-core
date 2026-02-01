using CrmCore.Infrastructure.Data.Task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrmCore.Infrastructure.Data.Task.Configurations;

public class TaskObserverConfiguration : IEntityTypeConfiguration<TaskObserverModel>
{
    public void Configure(EntityTypeBuilder<TaskObserverModel> builder)
    {
        builder.ToTable("task_observers");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        builder.HasIndex(x => new { x.TaskId, x.UserId })
            .IsUnique();

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