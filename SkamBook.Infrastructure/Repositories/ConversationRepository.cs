using Microsoft.EntityFrameworkCore;

using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Infrastructure.Context;

namespace SkamBook.Infrastructure.Repositories;

public class ConversationRepository : IConversationRepository
{
    private readonly SkamBookContext _context;

    public ConversationRepository(SkamBookContext context)
    {
        _context = context;
    }

    public async Task AddConversationAsync(Conversation conversation)
    {
        await _context.AddAsync(conversation);
    }

    public async Task<IEnumerable<Conversation>> GetConversation(Guid senderId, Guid receiverId)
    {
        var response = await _context.Conversations
                .Where(c =>
                    (c.SenderId.Equals(senderId) && c.ReceiverId.Equals(receiverId))
                    || (c.SenderId.Equals(receiverId) && c.ReceiverId.Equals(senderId)))
                .ToListAsync()
            ;

        return response;
    }
}
