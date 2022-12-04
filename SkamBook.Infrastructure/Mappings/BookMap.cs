using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkamBook.Core.Entities;

namespace SkamBook.Infrastructure.Mappings;

public class BookMap : IEntityTypeConfiguration<Book>
{

    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .HasKey(b => b.Id);

        builder
            .HasOne(b => b.User)
            .WithMany(b => b.Books)
            .HasForeignKey(b => b.UserId);
    }
}
