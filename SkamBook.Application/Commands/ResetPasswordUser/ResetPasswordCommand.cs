using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Commands.ResetPasswordUser;

public class ResetPasswordCommand : IRequest<ResponseViewModel>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
