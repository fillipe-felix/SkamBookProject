using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkamBook.Core.Entities;

namespace SkamBook.Infrastructure.Mappings;

public class UserCategoryMap : IEntityTypeConfiguration<UserCategory>
{


    public void Configure(EntityTypeBuilder<UserCategory> builder)
    {
        builder
            .HasKey(u => new { u.UserId, u.CategoryId });
        
        builder
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserCategories)
            .HasForeignKey(uc => uc.UserId);

        builder
            .HasOne(uc => uc.Category)
            .WithMany(u => u.UserCategories)
            .HasForeignKey(uc => uc.CategoryId);
    }
}

