using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Queries.BookQuery.BooksLiked;

public record BooksLikedQuery() : IRequest<ResponseViewModel>;
