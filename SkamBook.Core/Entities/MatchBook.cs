namespace SkamBook.Core.Entities;

public class MatchBook : BaseEntity
{
    public Guid BookId { get; set; }
    public Guid BookIdLiked { get; set; }
    public Guid UserId { get; set; }
    public Guid UserIdLiked { get; set; }
    public bool IsMatched { get; set; }
}
