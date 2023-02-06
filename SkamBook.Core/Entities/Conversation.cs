namespace SkamBook.Core.Entities;

public class Conversation : BaseEntity
{
    public DateTime Timestamp { get; set; }
    public string Message { get; set; }
    public Guid SenderId { get; set; }
    public virtual User Sender { get; set; }
    public Guid ReceiverId { get; set; }
    public virtual User Receiver { get; set; }
}
