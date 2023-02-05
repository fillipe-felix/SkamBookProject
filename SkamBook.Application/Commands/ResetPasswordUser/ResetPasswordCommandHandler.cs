using MediatR;

using Microsoft.AspNetCore.Identity;

using SkamBook.Application.ViewModels;
using SkamBook.Core.Interfaces;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Core.Interfaces.Services;
using SkamBook.Infrastructure.Context;

namespace SkamBook.Application.Commands.ResetPasswordUser;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResponseViewModel>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenPasswordRepository _tokenPasswordRepository;
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordCommandHandler(
        UserManager<ApplicationUser> userManager,
        ITokenPasswordRepository tokenPasswordRepository,
        IJwtService jwtService,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _tokenPasswordRepository = tokenPasswordRepository;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseViewModel> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.Password.Equals(request.ConfirmPassword))
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var tokenReset = await _tokenPasswordRepository.FindTokenPasswordByEmail(request.Email);

            var resultado = await _userManager.ResetPasswordAsync(user, tokenReset.Token, request.Password);

            if (resultado.Succeeded)
            {
                if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    var token = await _jwtService.GenerateJwtToken(request.Email);

                    await _tokenPasswordRepository.Delete(tokenReset);
                    await _unitOfWork.Commit();
                    
                    return new ResponseViewModel(true,
                        new LoginResponseViewModel(null, token));
                }
            }
            else
            {
                return new ResponseViewModel(false,
                    "Não foi possível redefinir a senha. Verifique se preencheu a senha corretamente. Se o problema persistir, entre em contato com o suporte.");
            }
        }

        return new ResponseViewModel(false, "Confirmação de senha inválida");
    }
}
