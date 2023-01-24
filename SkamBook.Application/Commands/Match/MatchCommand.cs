using System.Text.Json.Serialization;

using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Commands.Match;

public class MatchCommand : IRequest<ResponseViewModel>
{
    public Guid BookIdLiked { get; set; }

}
