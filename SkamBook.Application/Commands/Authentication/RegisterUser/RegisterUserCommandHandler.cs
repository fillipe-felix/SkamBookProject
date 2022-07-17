using MediatR;

using Microsoft.AspNetCore.Identity;

using SkamBook.Application.ViewModels;
using SkamBook.Core.Interfaces.Services;
using SkamBook.Infrastructure.Context;


namespace SkamBook.Application.Commands.Authentication.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResponseViewModel>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtService _authService;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, 
                                      SignInManager<ApplicationUser> signInManager, 
                                      IJwtService authService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
    }

    public async Task<ResponseViewModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userManager.FindByNameAsync(request.Email);

        if (userExists != null)
        {
            return new ResponseViewModel(false, new List<string>() { "Email já cadastrado." });
        }
           
        ApplicationUser userIdentity = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Email
        };
        var result = await _userManager.CreateAsync(userIdentity, request.Password);

        if (result.Succeeded)
        {
            var token = await _authService.GenerateJwtToken(request.Email);

            var login = new LoginResponseViewModel(null, token);
            
            await _signInManager.SignInAsync(userIdentity, false);
            
            return new ResponseViewModel(true, login);
        }

        return new ResponseViewModel(false, new List<string>() { "Falha na criação do usuário, por favor tente novamente!" });
    }
}
