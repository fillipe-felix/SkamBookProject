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
    private readonly IBookRepository _bookRepository;
    private readonly IUser _user;

    public FetchNearestQueryHandler(
        IUserRepository userRepository, 
        IGoogleService googleService, 
        IUser user, 
        IBookRepository bookRepository)
    {
        _userRepository = userRepository;
        _googleService = googleService;
        _user = user;
        _bookRepository = bookRepository;
    }

    public async Task<ResponseViewModel> Handle(FetchNearestQuery request, CancellationToken cancellationToken)
    {
        var email = _user.ObterUserEmail();
        
        var user = await _userRepository.GetUserByEmailWithAddressAsync(email);
        
        var listBooks = await _bookRepository.GetAllBooksToFetchNearestAsync(user.Id, user.Address.City);

        if (listBooks.Count == 0)
        {
            return new ResponseViewModel(false, "Não existe usuários próximos");
        }

        var nearestUsersAsync = await _googleService.FindNearestUsersAsync(user.Address.Lat, user.Address.Lon, listBooks);

        var result = new NearestUserViewModel();

        for (int i = 0; i < listBooks.Count; i++)
        {
            
                var imagesUrl = listBooks[i].BookImages.Select(s => s.Image.UrlImage);

                var book = new BookViewModel
                {
                    Id = listBooks[i].Id,
                    Name = listBooks[i].Name,
                    Author = listBooks[i].Author,
                    Description = listBooks[i].Description,
                    Images = imagesUrl,
                    UserId = listBooks[i].User.Id,
                    Address = new AddressViewModel { City = listBooks[i].User.Address.City },
                    Distance = nearestUsersAsync[i].Distance.Value,
                    DistanceString = nearestUsersAsync[i].Distance.Text,
                    UserImage = listBooks[i].User.ImageProfile.UrlImage
                };
                
                result.Books.Add(book);
            
        }

        var resultOrderedEnumerable = from r in result.Books
            orderby r.Distance
            select r;


        return new ResponseViewModel(true, resultOrderedEnumerable);
    }
}
