using System.Security.Claims;

using MediatR;

using SkamBook.Application.Extensions;
using SkamBook.Application.Queries.Match.FetchNearest;
using SkamBook.Application.ViewModels;
using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Infrastructure.Interfaces;

namespace SkamBook.Application.Commands.Match;

public class MatchCommandHandler : IRequestHandler<MatchCommand, ResponseViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMatchRepository _matchRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUser _user;

    public MatchCommandHandler(IUserRepository userRepository, IBookRepository bookRepository, IMatchRepository matchRepository, IUnitOfWork unitOfWork, IUser user)
    {
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _matchRepository = matchRepository;
        _unitOfWork = unitOfWork;
        _user = user;
    }

    public async Task<ResponseViewModel> Handle(MatchCommand request, CancellationToken cancellationToken)
    {
        var email = _user.ObterUserEmail();
        
        var user = await _userRepository.GetUserByEmailWithBooksAsync(email);

        if (user is null) 
        {
            return new ResponseViewModel(false, "Usuário não encontrado");
        }

        var bookLiked = await _bookRepository.GetBookByIdAsync(request.BookIdLiked);

        if (bookLiked is null)
        {
            return new ResponseViewModel(false, "Livro não encontrado");
        }

        var match = new MatchBook();
        
        foreach (var userBook in user.Books)
        {
            match = await _matchRepository.GetMatchByIdBooks(userBook.Id, bookLiked.Id);
        
            if (match == null)
            {
                match = new MatchBook
                {
                    BookId = userBook.Id,
                    BookIdLiked = bookLiked.Id,
                    UserId = userBook.UserId,
                    UserIdLiked = bookLiked.UserId
                };
                await _matchRepository.AddMatchBookAsync(match);
            }
            else
            {
                match.IsMatched = true;
                await _matchRepository.UpdateMatchBookAsync(match);
            }

            await _unitOfWork.Commit();
        }

        return new ResponseViewModel(true, match);
    }

    
}
