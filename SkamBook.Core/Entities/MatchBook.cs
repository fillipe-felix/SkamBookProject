namespace SkamBook.Core.Entities;

public class MatchBook : BaseEntity
{
    public Guid UserLikeId { get; set; }
    public Guid UserLikedId { get; set; }
    
    public Guid BookLikeId { get; set; }
    public Guid BookLikedId { get; set; }
    public bool IsMatched { get; set; }
    
    public virtual Book BookLike { get; set; }
    public virtual Book BookLiked { get; set; }
}
