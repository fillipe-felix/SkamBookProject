using MediatR;

using SkamBook.Application.ViewModels;
using SkamBook.Application.ViewModels.Users;
using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces;
using SkamBook.Core.Interfaces.Repositories;

namespace SkamBook.Application.Commands.UserEntity.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = new User(
            request.FullName, 
            request.BirthDate, 
            request.Email,
            request.Lat, 
            request.Lon, 
            request.CategoriesId);

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
