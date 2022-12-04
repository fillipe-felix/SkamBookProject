using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Commands.ForgotPasswordUser;

public class ForgotPasswordUserCommand : IRequest<ResponseViewModel>
{
    public string Email { get; set; }
}
