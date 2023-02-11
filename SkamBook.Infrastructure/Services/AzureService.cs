using System.Text.RegularExpressions;

using Azure.Storage.Blobs;

using SkamBook.Infrastructure.Interfaces;
using SkamBook.Infrastructure.Settings;

namespace SkamBook.Infrastructure.Services;

public class AzureService : IAzureService
{
    private readonly ApiSettings _apiSettings;

    public AzureService(ApiSettings apiSettings)
    {
        _apiSettings = apiSettings;
    }

    public async Task<IEnumerable<string>> UploadBase64Image(List<string> base64Images, string container)
    {
        var urls = new List<string>();
        
        // Limpa o hash enviado
        var data = base64Images.Select(i => new Regex(@"^data:image\/[a-z]+;base64,").Replace(i, "")).ToList();

        foreach (var d in data)
        {
            // Gera um nome randomico para imagem
            var fileName = Guid.NewGuid() + ".jpg";
            // Gera um array de Bytes
            byte[] imageBytes = Convert.FromBase64String(d);
    
            // Define o BLOB no qual a imagem será armazenada
            var blobClient = new BlobClient(_apiSettings.KeyStorageAzure, container, fileName);

            // Envia a imagem
            using(var stream = new MemoryStream(imageBytes)) {
                await blobClient.UploadAsync(stream, overwrite: true);
            }
            
            urls.Add(blobClient.Uri.AbsoluteUri);
        }

        return urls;
    }
}
