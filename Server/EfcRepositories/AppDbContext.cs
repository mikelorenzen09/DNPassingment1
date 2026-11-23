namespace EfcRepositories;

using Entities;
using Microsoft.EntityFrameworkCore;

// Named AppDbContext rather than the assignment's own "AppContext" - that name
// collides with System.AppContext, which the Web SDK's implicit usings bring into
// scope, and produces an "ambiguous reference" compile error as soon as you type
// a bare "AppContext" anywhere (e.g. registering it in Program.cs).
public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(PostgresConnectionSettings.ConnectionString);
    }
}
