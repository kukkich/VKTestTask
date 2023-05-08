using Microsoft.EntityFrameworkCore;

namespace VKTestTask.Domain;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserGroup> UserGroups { get; set; } = null!;
    public DbSet<UserState> UserStates { get; set; } = null!;

    public ApplicationContext(DbContextOptions options) 
        : base(options)
    {
        if (!Database.EnsureCreated()) return;
    }
}