using System.Text.Json.Serialization;

namespace SkamBook.Application.ViewModels;

public class ResponseViewModel
{
    public ResponseViewModel(bool success, List<string> erros)
    {
        Success = success;
        Erros = erros;
    }

    public ResponseViewModel(bool success, object data)
    {
        Success = success;
        Data = data;
    }

    public bool Success { get; private set; }
    
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string> Erros { get; private set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object Data { get; private set; }
}

