using System.Reflection;

using Microsoft.EntityFrameworkCore;

using SkamBook.Core.Entities;
using SkamBook.Infrastructure.Mappings;

namespace SkamBook.Infrastructure.Context;

public class SkamBookContext : DbContext
{
    public SkamBookContext(DbContextOptions<SkamBookContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<MatchBook> Matches { get; set; }
    
    public DbSet<Image> Images { get; set; }
    public DbSet<TokenPassword> TokenPasswords { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .OwnsOne(u => u.Email);

        modelBuilder.ApplyConfiguration(new ConversationMap());
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
