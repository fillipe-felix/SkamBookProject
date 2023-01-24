using SkamBook.Core.Entities;

namespace SkamBook.Application.ViewModels;

public class NearestUserViewModel
{
    public List<BookViewModel> Books { get; set; } = new List<BookViewModel>();
}
