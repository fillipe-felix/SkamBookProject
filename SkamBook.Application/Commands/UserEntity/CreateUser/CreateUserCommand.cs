using MediatR;

using SkamBook.Application.ViewModels;
using SkamBook.Core.Entities;

namespace SkamBook.Application.Commands.UserEntity.CreateUser;

public record CreateUserCommand(
    string Email, 
    DateTime BirthDate, 
    string FullName,
    long Lat,
    long Lon,
    List<Guid> CategoriesId) : IRequest<ResponseViewModel>;
