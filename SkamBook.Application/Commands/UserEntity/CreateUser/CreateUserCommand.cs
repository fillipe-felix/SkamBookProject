using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Commands.UserEntity.CreateUser;

public class CreateUserCommand : IRequest<ResponseViewModel>
{
    public DateTime BirthDate { get; set; }
    public string FullName { get; set; }
    public string Lat { get; set; }
    public string Lon { get; set; }
    public string? ImageProfile { get; set; }
    public List<Guid> CategoriesId { get; set; }
}
