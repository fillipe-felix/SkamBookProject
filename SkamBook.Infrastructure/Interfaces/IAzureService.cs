namespace SkamBook.Infrastructure.Interfaces;

public interface IAzureService
{
    Task<IEnumerable<string>> UploadBase64Image(List<string> base64Images, string container);
}
