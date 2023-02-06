using SkamBook.Core.Entities;

namespace SkamBook.Core.Interfaces.Repositories;

public interface IConversationRepository
{
    Task AddConversationAsync(Conversation conversation);
    Task<IEnumerable<Conversation>> GetConversation(Guid senderId, Guid receiverId);
}
