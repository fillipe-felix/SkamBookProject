using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkamBook.Core.Entities;

public class MatchBookMap : IEntityTypeConfiguration<MatchBook>
{
    public void Configure(EntityTypeBuilder<MatchBook> builder)
    {
        // builder.HasKey(mb => new { UserId = mb.UserLikedId, BookId = mb.BookUserId });
        // builder.HasOne(x => x.UserLiked)
        //     .WithMany(x => x.LikedBooks)
        //     .HasForeignKey(x => x.UserLikedId)
        //     .OnDelete(DeleteBehavior.Restrict);
        // builder.HasOne(x => x.UserLike)
        //     .WithMany(x => x.LikedBooks)
        //     .HasForeignKey(x => x.UserLikeId)
        //     .OnDelete(DeleteBehavior.Restrict);
        // builder.HasOne(x => x.BookUser)
        //     .WithMany(x => x.LikedBy)
        //     .HasForeignKey(x => x.BookUserId)
        //     .OnDelete(DeleteBehavior.Restrict);
        // builder.HasOne(x => x.BookUserLiked)
        //     .WithMany(x => x.LikedBy)
        //     .HasForeignKey(x => x.BookUserLikedId)
        //     .OnDelete(DeleteBehavior.Restrict);
    }
}
