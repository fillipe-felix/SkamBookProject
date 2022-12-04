using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkamBook.Core.Entities;

namespace SkamBook.Infrastructure.Mappings;

public class BookImageMap : IEntityTypeConfiguration<BookImage>
{


    public void Configure(EntityTypeBuilder<BookImage> builder)
    {
        builder
            .HasKey(u => new { u.ImageId, u.BookId });
        
        builder
            .HasOne(uc => uc.Book)
            .WithMany(u => u.BookImages)
            .HasForeignKey(uc => uc.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(uc => uc.Image)
            .WithMany(u => u.BookImages)
            .HasForeignKey(uc => uc.ImageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

