using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ModelX;

public class BlogsContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(@"Data Source=(LocalDb)\MSSQLLocalDB;Database=Blogs");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Blog>()
            .HasIndex(e => e.Name);

        modelBuilder
            .Entity<Account>()
            .OwnsOne(p => p.Details)
            .ToJson();
    }
}
