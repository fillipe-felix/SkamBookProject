using System.Text.RegularExpressions;

using FluentValidation;

using SkamBook.Application.Commands.Authentication.RegisterUser;

namespace SkamBook.Application.Validators;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(p => p.Email)
            .EmailAddress()
            .WithMessage("E-mail não é válido!");

        RuleFor(p => p.Password)
            .Must(ValidPassword)
            .WithMessage("Senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula, e um caractere esppcial.");

        RuleFor(p => p.ConfirmPassword)
            .Must((model, field) => model.Password.Equals(model.ConfirmPassword))
            .WithMessage("As senhas não são iguais.");
    }
    
    public bool ValidPassword(string password)
    {
        var regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$");

        return regex.IsMatch(password);
    }
}
