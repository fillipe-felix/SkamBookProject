using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkamBook.Core.Entities;

namespace SkamBook.Infrastructure.Mappings;

public class TokenPasswordMap : IEntityTypeConfiguration<TokenPassword>
{

    public void Configure(EntityTypeBuilder<TokenPassword> builder)
    {
        builder
            .HasKey(i => i.Id);
    }
}
