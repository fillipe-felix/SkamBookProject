using System.Text.RegularExpressions;

namespace SkamBook.Core.Entities;


public class Email : BaseEntity
{
    public const int EnderecoMaxLength = 254;
    public const int EnderecoMinLength = 5;
    
    public string Endereco { get; private set; }

    protected Email()
    {
        
    }
    
    public bool ValidarEmail(string endereco)
    {
        if (!Validar(endereco))
        {
            return false;
        }
        Endereco = endereco;
        return true;
    }
    
    public static bool Validar(string email)
    {
        var regexEmail =
            new Regex(
                @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-z]\.)+[a-zA-Z]{2,6}))$");
        return regexEmail.IsMatch(email);
    }
}
