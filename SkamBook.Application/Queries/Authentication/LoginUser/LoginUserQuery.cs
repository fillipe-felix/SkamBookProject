using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Queries.Authentication.LoginUser;

public record LoginUserQuery(string Password = null!, string Email = null!) : IRequest<ResponseViewModel>;
