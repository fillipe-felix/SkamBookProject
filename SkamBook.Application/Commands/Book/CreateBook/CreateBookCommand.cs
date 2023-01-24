using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Commands.Book.CreateBook;

public record CreateBookCommand(
    string name,
    string author,
    string description,
    List<Guid> CategoriesId,
    List<string> base64Images) : IRequest<ResponseViewModel>;
