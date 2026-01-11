using CrmCore.Infrastructure.Data.Task.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrmCore.Infrastructure.Data.Task.Configurations;

public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskCommentModel>
{
    public void Configure(EntityTypeBuilder<TaskCommentModel> builder)
    {
        builder.ToTable("task_comments");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        builder.HasIndex(x => new { x.TaskId, x.UserId })
            .IsUnique();

        builder.HasOne(x => x.Task)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.TaskId);

        builder.Property(x => x.Value)
            .HasMaxLength(255);

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