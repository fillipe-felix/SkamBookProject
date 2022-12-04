using System.Security.Claims;

using MediatR;

using SkamBook.Application.ViewModels;
using SkamBook.Application.ViewModels.Users;
using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Infrastructure.Interfaces;

namespace SkamBook.Application.Commands.UserEntity.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAzureService _azureService;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IAzureService azureService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _azureService = azureService;
    }

    public async Task<ResponseViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var imageUrl = await _azureService.UploadBase64Image(new List<string> { request.ImageProfile }, "skambookcontainer");
        
        var userEntity = new User(
            request.FullName, 
            request.BirthDate, 
            request.Email,
            request.Lat, 
            request.Lon, 
            request.CategoriesId, 
            new Image(imageUrl.FirstOrDefault()));

        
        
        await _userRepository.AddUserAsync(userEntity);
        
        var response = await _unitOfWork.Commit();

        if (response)
        {
            var userViewModel = new UserViewModel(request.FullName, request.Email);
            
            return new ResponseViewModel(true, userViewModel);
        }

        return new ResponseViewModel(false, new List<string> { "Falha ao realizar cadastro de usuário" });
    }
}
