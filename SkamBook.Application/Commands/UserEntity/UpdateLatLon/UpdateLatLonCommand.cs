using MediatR;

using SkamBook.Application.ViewModels;

namespace SkamBook.Application.Commands.UserEntity.UpdateLatLon;

public class UpdateLatLonCommand : IRequest<ResponseViewModel>
{
    public string Lat { get; set; }
    public string Lon { get; set; }
}
