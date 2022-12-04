using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkamBook.Core.Entities;

namespace SkamBook.Infrastructure.Mappings;

public class ImageMap : IEntityTypeConfiguration<Image>
{

    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder
            .HasKey(i => i.Id);
    }
}
