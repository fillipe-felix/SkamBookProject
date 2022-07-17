namespace SkamBook.Core.Entities;

public class User : BaseEntity
{
    protected User()
    {
        
    }
    
    public string Name { get; private set; } = null!;
    public DateTime BirthDate { get; private set; }
    public Email Email { get; private set; } = null!;
    public bool Active { get; private set; }
    public Address Address { get; private set; } = null!;

    public void AddUser(User user)
    {
        if (string.IsNullOrEmpty(user.Name)) throw new InvalidOperationException("Nome não pode estar vazio");
        if (user.BirthDate.Equals(null)) throw new InvalidOperationException("Data de nascimento não pode estar vazia");
        
    }
}
