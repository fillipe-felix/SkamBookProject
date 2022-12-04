using MediatR;

using SkamBook.Application.Commands.ForgotPasswordUser;
using SkamBook.Application.ViewModels;
using SkamBook.Core.Interfaces.Repositories;

namespace SkamBook.Application.Commands.ValidRandomToken;

public class ValidRandomTokenCommandHandler : IRequestHandler<ValidRandomTokenCommand, ResponseViewModel>
{

    private readonly ITokenPasswordRepository _tokenPasswordRepository;

    public ValidRandomTokenCommandHandler(ITokenPasswordRepository tokenPasswordRepository)
    {
        _tokenPasswordRepository = tokenPasswordRepository;
    }

    public async Task<ResponseViewModel> Handle(ValidRandomTokenCommand request, CancellationToken cancellationToken)
    {
        var token = await _tokenPasswordRepository.FindTokenPasswordByRandomPassword(request.RandomToken);

        if (string.IsNullOrEmpty(token.RandomPassword))
        {
            return new ResponseViewModel(false, "Código inválido");
        }

        return new ResponseViewModel(true, "Código válido");
    }
    
}
