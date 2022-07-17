using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkamBook.Core.Entities;

namespace SkamBook.Infrastructure.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .HasOne(u => u.Address);
    }
}
