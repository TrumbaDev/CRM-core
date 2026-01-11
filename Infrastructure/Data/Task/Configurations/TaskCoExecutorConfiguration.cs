using CrmCore.Infrastructure.Data.Task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrmCore.Infrastructure.Data.Task.Configurations;

public class TaskCoExecutorConfiguration : IEntityTypeConfiguration<TaskCoExecutorModel>
{
    public void Configure(EntityTypeBuilder<TaskCoExecutorModel> builder)
    {
        builder.ToTable("task_co_executors");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        builder.HasIndex(x => new { x.TaskId, x.UserId })
            .IsUnique();

        builder.HasOne(x => x.Task)
            .WithMany(x => x.CoExecutors)
            .HasForeignKey(x => x.TaskId);

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