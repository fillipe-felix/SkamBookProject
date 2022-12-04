using System.Text;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using SkamBook.Application.ViewModels;
using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Core.Interfaces.Services;
using SkamBook.Infrastructure.Context;

namespace SkamBook.Application.Commands.ForgotPasswordUser;

public class ForgotPasswordUserCommandHandler : IRequestHandler<ForgotPasswordUserCommand, ResponseViewModel>
{
    
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly ITokenPasswordRepository _tokenPasswordRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ForgotPasswordUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        IEmailService emailService,
        ITokenPasswordRepository tokenPasswordRepository,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _emailService = emailService;
        _tokenPasswordRepository = tokenPasswordRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseViewModel> Handle(ForgotPasswordUserCommand request, CancellationToken cancellationToken)
    {
        if (_userManager.Users.AsNoTracking().Any(u => u.NormalizedEmail == request.Email.ToUpper().Trim()))
        {
            var usuario = await _userManager.FindByEmailAsync(request.Email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
            var randomToken = GenerateRandomPassword();

            await _tokenPasswordRepository.Add(new TokenPassword(request.Email, token, randomToken));
            await _unitOfWork.Commit();
            

            var mensagem = new StringBuilder();
            mensagem.Append($"<p>Olá, {usuario.UserName }.</p>");
            mensagem.Append("<p>Houve uma solicitação de redefinição de senha para seu usuário em nosso app. Se não foi você que fez a solicitação, ignore essa mensagem. Caso tenha sido você, digite o código abaixo para criar sua nova senha:</p>");
            mensagem.Append($"<p>{randomToken}</p>");
            mensagem.Append("<p>Atenciosamente,<br>Equipe de Suporte Skambook</p>");
            
            await _emailService.SendEmailAsync(usuario.Email,
                "Redefinição de Senha", "", mensagem.ToString());

            return new ResponseViewModel(true, "");
        }

        return new ResponseViewModel(false, "E-mail não cadastrado");
    }


    private string GenerateRandomPassword()
    {
        string chars = "abcdefghjkmnpqrstuvwxyz023456789";
        string pass = "";
        Random random = new Random();
        for (int f = 0; f < 6; f++) {
            pass += chars.Substring(random.Next(0, chars.Length-1), 1);
        }

        return pass;
    }
}
