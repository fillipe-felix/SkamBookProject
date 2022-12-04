using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Commands.UserEntity.CreateUser;

public class CreateUserCommand : IRequest<ResponseViewModel>
{
    public DateTime BirthDate { get; set; }
    public string FullName { get; set; }
    public long Lat { get; set; }
    public long Lon { get; set; }
    public string ImageProfile { get; set; }
    public List<Guid> CategoriesId { get; set; }
    internal string Email { get; set; }

    public void SetEmail(string email)
    {
        Email = email;
    }
}
