﻿using System.Reflection;

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


        modelBuilder.Entity<Book>()
        .HasMany(b => b.LikedBy)
        .WithOne(m => m.BookLiked)
        .HasForeignKey(m => m.BookLikedId);
        
        modelBuilder.Entity<MatchBook>()
        .HasOne(m => m.BookLiked)
        .WithMany(b => b.LikedBy)
        .HasForeignKey(m => m.BookLikedId)
        .OnDelete(DeleteBehavior.NoAction);

    modelBuilder.Entity<Book>()
        .HasMany(b => b.LikedBooks)
        .WithOne(m => m.BookLike)
        .HasForeignKey(m => m.BookLikeId);
    
    modelBuilder.Entity<MatchBook>()
        .HasOne(m => m.BookLike)
        .WithMany(b => b.LikedBooks)
        .HasForeignKey(m => m.BookLikeId)
        .OnDelete(DeleteBehavior.NoAction);
    
    



    modelBuilder.ApplyConfiguration(new ConversationMap());
    //modelBuilder.ApplyConfiguration(new MatchBookMap());
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
