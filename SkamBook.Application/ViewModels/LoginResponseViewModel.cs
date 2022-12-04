using SkamBook.Application.ViewModels.Users;

namespace SkamBook.Application.ViewModels;

public record LoginResponseViewModel(UserViewModel UserViewModel, string AccessToken);
