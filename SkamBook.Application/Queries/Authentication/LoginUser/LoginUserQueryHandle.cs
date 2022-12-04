using System.Security.Claims;

using MediatR;

using Microsoft.AspNetCore.Identity;

using SkamBook.Application.Queries.Authentication.LoginUser;
using SkamBook.Application.ViewModels;
using SkamBook.Application.ViewModels.Users;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Core.Interfaces.Services;
using SkamBook.Infrastructure.Context;

namespace SkamBook.Application.Queries.User.LoginUser;

public class LoginUserQueryHandle : IRequestHandler<LoginUserQuery, ResponseViewModel>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;

    public LoginUserQueryHandle(UserManager<ApplicationUser> userManager,
                                IJwtService jwtService,
                                IUserRepository userRepository)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _userRepository = userRepository;
    }

    public async Task<ResponseViewModel> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var userIdentity = await _userManager.FindByNameAsync(request.Email);
        if (userIdentity != null && await _userManager.CheckPasswordAsync(userIdentity, request.Password))
        {
            var token = await _jwtService.GenerateJwtToken(request.Email);
            
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user is null)
            {
                return new ResponseViewModel(true, new LoginResponseViewModel(null, token));
            }

            var userViewModel = new UserViewModel(user.FullName, user.Email.Endereco);
            
            var login = new LoginResponseViewModel(userViewModel, token);

            return new ResponseViewModel(true, login);
        }
        return new ResponseViewModel(false, new List<string>{"Email ou senha inválidos"});
    }
}
