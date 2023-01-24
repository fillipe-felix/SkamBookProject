using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Queries.Match.FetchNearest;

public record FetchNearestQuery() : IRequest<ResponseViewModel>;
