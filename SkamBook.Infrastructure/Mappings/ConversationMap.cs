using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SkamBook.Core.Entities;

namespace SkamBook.Infrastructure.Mappings;

public class ConversationMap : IEntityTypeConfiguration<Conversation>
{

    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder
            .HasKey(u => u.Id);
        
        builder.HasOne(c => c.Sender)
            .WithMany(u => u.Conversations)
            .HasForeignKey(c => c.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Receiver)
            .WithMany()
            .HasForeignKey(c => c.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
