using CrmCore.Infrastructure.Data.Task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrmCore.Infrastructure.Data.Task.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskModel>
{
    public void Configure(EntityTypeBuilder<TaskModel> builder)
    {
        builder.ToTable("tasks");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.Title)
            .HasMaxLength(255);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.DeadLine)
            .IsRequired();

        builder.Property(x => x.ExecutorId)
            .IsRequired();

        builder.HasOne(x => x.Executor)
            .WithMany()
            .HasForeignKey(x => x.ExecutorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.DirectorId)
            .IsRequired();

        builder.HasOne(x => x.Director)
            .WithMany()
            .HasForeignKey(x => x.DirectorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.CoExecutors)
            .WithOne(x => x.Task)
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Observers)
            .WithOne(x => x.Task)
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Task)
            .HasForeignKey(x => x.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

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