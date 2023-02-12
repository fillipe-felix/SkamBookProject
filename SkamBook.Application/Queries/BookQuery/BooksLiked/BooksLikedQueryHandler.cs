using MediatR;

using SkamBook.Application.Extensions;
using SkamBook.Application.ViewModels;
using SkamBook.Core.Interfaces.Repositories;

namespace SkamBook.Application.Queries.BookQuery.BooksLiked;

public class BooksLikedQueryHandler : IRequestHandler<BooksLikedQuery, ResponseViewModel>
{
    
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUser _user;

    public BooksLikedQueryHandler(
        IUser user, 
        IBookRepository bookRepository,
        IUserRepository userRepository)
    {
        _user = user;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
    }

    public async Task<ResponseViewModel> Handle(BooksLikedQuery request, CancellationToken cancellationToken)
    {
        var userId = _user.ObterUserId();

        var listBooksIds = await _bookRepository.GetBooksLikedIdById(userId);
        var listBooks = await _bookRepository.GetAllBookById(listBooksIds);

        if (listBooks.Count() <= 0)
        {
            return new ResponseViewModel(false, "Usuário não curtiu nenhum livro");
        }
        

        var response = listBooks.Select(b => new
        {
            Id = b.Id,
            Name = b.Name,
            Author = b.Author,
            Description = b.Description,
            Images = b.BookImages.Select(s => s.Image.UrlImage)
        });
        
        return new ResponseViewModel(true, response);
    }
}
