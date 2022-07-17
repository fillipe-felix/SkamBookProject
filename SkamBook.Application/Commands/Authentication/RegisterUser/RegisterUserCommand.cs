using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Commands.Authentication.RegisterUser;

public record RegisterUserCommand(
    string Email, 
    string Password, 
    string ConfirmPassword) : IRequest<ResponseViewModel>;
