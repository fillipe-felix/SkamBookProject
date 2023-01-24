using MediatR;

using SkamBook.Application.Extensions;
using SkamBook.Application.ViewModels;
using SkamBook.Core.Entities;
using SkamBook.Core.Interfaces;
using SkamBook.Core.Interfaces.Repositories;
using SkamBook.Infrastructure.Interfaces;

namespace SkamBook.Application.Commands.Book.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, ResponseViewModel>
{
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IAzureService _azureService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUser _user;

    public CreateBookCommandHandler(
        IUserRepository userRepository, 
        IUnitOfWork unitOfWork, 
        IAzureService azureService, 
        IBookRepository bookRepository, 
        IImageRepository imageRepository, 
        IUser user)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _azureService = azureService;
        _bookRepository = bookRepository;
        _imageRepository = imageRepository;
        _user = user;
    }

    public async Task<ResponseViewModel> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var email = _user.ObterUserEmail();

        var user = await _userRepository.GetUserByEmailAsync(email);
        
        var linkImagens = await _azureService.UploadBase64Image(request.base64Images, "skambookcontainer");

        var images = linkImagens.Select(c => new Image(c)).ToList();

        var book = new Core.Entities.Book(
            user.Id, 
            request.name, 
            request.author, 
            request.description, 
            request.CategoriesId, 
            images.Select(c => c.Id).ToList());

        await _imageRepository.Add(images);
        await _bookRepository.Add(book);
        
        var result = await _unitOfWork.Commit();

        if (result)
        {
            return new ResponseViewModel(true, "Livros cadastrados com sucesso!");
        }

        return new ResponseViewModel(false, new List<string> { "Falha ao realizar cadastro de usuário" });
    }
}