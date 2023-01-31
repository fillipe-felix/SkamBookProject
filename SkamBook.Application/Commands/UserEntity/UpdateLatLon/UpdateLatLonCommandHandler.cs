using MediatR;

using SkamBook.Application.Commands.UserEntity.CreateUser;
using SkamBook.Application.Extensions;
using SkamBook.Application.ViewModels;
using SkamBook.Application.ViewModels.Users;
using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Infrastructure.Interfaces;

namespace SkamBook.Application.Commands.UserEntity.UpdateLatLon;

public class UpdateLatLonCommandHandler : IRequestHandler<UpdateLatLonCommand, ResponseViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGoogleService _googleService;
    private readonly IUser _user;

    public UpdateLatLonCommandHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork,
        IGoogleService googleService,
        IUser user)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _googleService = googleService;
        _user = user;
    }

    public async Task<ResponseViewModel> Handle(UpdateLatLonCommand request, CancellationToken cancellationToken)
    {
        var email = _user.ObterUserEmail();

        var user = await _userRepository.GetUserByEmailWithAddressAsync(email);

        if (user is null)
        {
            return new ResponseViewModel(false, new List<string> { "Usuário não encontrado" });
        }
        
        var city = await _googleService.GetCityUserByLatLonAsync(request.Lat, request.Lon);

        user.Address.UpdateLatLon(request.Lat, request.Lon, city);

        var response = await _unitOfWork.Commit();

        if (response)
        {
            return new ResponseViewModel(true, "Latitude e longitude atualizadas");
        }

        return new ResponseViewModel(false, new List<string> { "Falha ao atualizar latitude e longitude" });
    }
}
