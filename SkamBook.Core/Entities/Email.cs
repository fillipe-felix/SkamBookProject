namespace SkamBook.Core.Entities;


public class Email : BaseEntity
{
    public const int EnderecoMaxLength = 254;
    public const int EnderecoMinLength = 5;
    
    public string Endereco { get; private set; }

    protected Email()
    {
        
    }

    public Email(string endereco)
    {
        Endereco = endereco;
    }
}
