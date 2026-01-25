using CrmCore.Infrastructure.Data.Task.Models;
using Microsoft.EntityFrameworkCore;

namespace CrmCore.Infrastructure.Data.Task;

public class TaskDbContext : DbContext
{
    public DbSet<TaskModel> Task => Set<TaskModel>();
    public DbSet<TaskCoExecutorModel> CoExecutor => Set<TaskCoExecutorModel>();
    public DbSet<TaskCommentModel> Comment => Set<TaskCommentModel>();
    public DbSet<TaskObserverModel> Observer => Set<TaskObserverModel>();

    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskDbContext).Assembly);
    }
}