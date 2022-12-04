using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Commands.ValidRandomToken;

public class ValidRandomTokenCommand : IRequest<ResponseViewModel>
{
    public string RandomToken { get; set; }
}
