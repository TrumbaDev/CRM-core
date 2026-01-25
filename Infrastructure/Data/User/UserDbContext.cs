using CrmCore.Infrastructure.Data.User.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrmCore.Infrastructure.Data.User;

public class UserDbContext : DbContext
{
    public DbSet<UserModel> Users => Set<UserModel>();

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
    }
}