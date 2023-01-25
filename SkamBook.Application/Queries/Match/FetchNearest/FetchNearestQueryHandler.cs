using System.Security.Claims;

using MediatR;

using SkamBook.Application.Extensions;
using SkamBook.Application.ViewModels;
using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Core.Interfaces.Services;
using SkamBook.Infrastructure.Interfaces;

namespace SkamBook.Application.Queries.Match.FetchNearest;

public class FetchNearestQueryHandler : IRequestHandler<FetchNearestQuery, ResponseViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IGoogleService _googleService;
    private readonly IUser _user;

    public FetchNearestQueryHandler(IUserRepository userRepository, IGoogleService googleService, IUser user)
    {
        _userRepository = userRepository;
        _googleService = googleService;
        _user = user;
    }

    public async Task<ResponseViewModel> Handle(FetchNearestQuery request, CancellationToken cancellationToken)
    {
        var email = _user.ObterUserEmail();
        
        var user = await _userRepository.GetUserByEmailWithAddressAsync(email);

        var listUser = await _userRepository.GetAllUserByCityAddressWithBooksAsync(user.Address.City, user.Email.Endereco);
        var nearestUsersAsync = await _googleService.FindNearestUsersAsync(user.Address.Lat, user.Address.Lon, listUser);

        var result = new NearestUserViewModel();

        for (int i = 0; i < listUser.Count; i++)
        {
            foreach (var book1 in listUser[i].Books)
            {
                var imagesUrl = book1.BookImages.Select(s => s.Image.UrlImage);

                var book = new BookViewModel
                {
                    Id = book1.Id,
                    Name = book1.Name,
                    Author = book1.Author,
                    Description = book1.Description,
                    Images = imagesUrl,
                    UserId = listUser[i].Id,
                    Address = new AddressViewModel { City = listUser[i].Address.City },
                    Distance = nearestUsersAsync[i].Distance.Value,
                    DistanceString = nearestUsersAsync[i].Distance.Text,
                    UserImage = book1.User.ImageProfile.UrlImage
                };
                
                result.Books.Add(book);
            }
        }

        var resultOrderedEnumerable = from r in result.Books
            orderby r.Distance
            select r;


        return new ResponseViewModel(true, resultOrderedEnumerable);
    }
}
