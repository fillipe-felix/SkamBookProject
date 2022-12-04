using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkamBook.Core.Entities;

namespace SkamBook.Infrastructure.Mappings;

public class BookCategoryMap : IEntityTypeConfiguration<BookCategory>
{


    public void Configure(EntityTypeBuilder<BookCategory> builder)
    {
        builder
            .HasKey(u => new { u.BookId, u.CategoryId });
        
        builder
            .HasOne(uc => uc.Book)
            .WithMany(u => u.BookCategories)
            .HasForeignKey(uc => uc.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(uc => uc.Category)
            .WithMany(u => u.BookCategories)
            .HasForeignKey(uc => uc.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

