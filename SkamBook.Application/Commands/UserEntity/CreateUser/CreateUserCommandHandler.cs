using System.Security.Claims;

using MediatR;

using SkamBook.Application.Extensions;
using SkamBook.Application.ViewModels;
using SkamBook.Application.ViewModels.Users;
using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Core.Interfaces.Services;
using SkamBook.Infrastructure.Interfaces;

namespace SkamBook.Application.Commands.UserEntity.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAzureService _azureService;
    private readonly IGoogleService _googleService;
    private readonly IUser _user;

    public CreateUserCommandHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork, 
        IAzureService azureService, 
        IGoogleService googleService,
        IUser user)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _azureService = azureService;
        _googleService = googleService;
        _user = user;
    }

    public async Task<ResponseViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = _user.ObterUserEmail();

        var user = await _userRepository.GetUserByEmailAsync(email);

        if (user != null)
        {
            return new ResponseViewModel(false, new List<string> { "Usuário já esta cadastrado" });
        }
        
        var imageUrl = !string.IsNullOrEmpty(request.ImageProfile) ?
        await _azureService.UploadBase64Image(new List<string> { request.ImageProfile }, "skambookcontainer") : new List<string>();

        var city = await _googleService.GetCityUserByLatLonAsync(request.Lat, request.Lon);

        var userEntity = new User(
            request.FullName,
            request.BirthDate,
            email,
            request.Lat,
            request.Lon,
            city,
            request.CategoriesId, 
            new Image(imageUrl.FirstOrDefault()));

        
        
        await _userRepository.AddUserAsync(userEntity);
        
        var response = await _unitOfWork.Commit();

        if (response)
        {
            var userViewModel = new UserViewModel(request.FullName, email);
            
            return new ResponseViewModel(true, userViewModel);
        }

        return new ResponseViewModel(false, new List<string> { "Falha ao realizar cadastro de usuário" });
    }
}
