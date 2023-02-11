namespace SkamBook.Core.Entities;

public class MatchBook : BaseEntity
{
    public Guid UserIdLiked { get; set; }
    public Guid BookIdLiked { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public bool IsMatched { get; set; }
    
    public virtual User User { get; set; }
    public virtual Book Book { get; set; }
}
