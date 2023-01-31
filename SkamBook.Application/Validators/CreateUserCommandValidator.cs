using FluentValidation;

using SkamBook.Application.Commands.UserEntity.CreateUser;

namespace SkamBook.Application.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(c => c.BirthDate)
            .NotNull()
            .NotEmpty()
            .WithMessage("Data de aniversário é obrigatório.");
        
        RuleFor(c => c.FullName)
            .NotNull()
            .NotEmpty()
            .WithMessage("Nome é obrigatório.");
        
        // RuleFor(c => c.Lat)
        //     .NotNull()
        //     .NotEmpty()
        //     .WithMessage("Localização é obrigatório.");
        //
        // RuleFor(c => c.Lon)
        //     .NotNull()
        //     .NotEmpty()
        //     .WithMessage("Localização é obrigatório.");

        RuleFor(c => c.CategoriesId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Por favor selecione pelo menos 3 categorias.");
    }
}
