using FluentValidation;

using SkamBook.Application.Queries.Authentication.LoginUser;

namespace SkamBook.Application.Validators;

public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator()
    {
        RuleFor(u => u.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo e-mail é obrigatório.");
        
        RuleFor(u => u.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage("O campo senha é obrigatório.");
    }
}
