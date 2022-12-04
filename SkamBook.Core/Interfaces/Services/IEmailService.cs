namespace SkamBook.Core.Interfaces.Services;

public interface IEmailService
{

    Task SendEmailAsync(string usuarioEmail, string assunto, string mensagemTexto, string mensagemHtml);
}
