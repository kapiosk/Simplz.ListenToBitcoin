using System.Text.Json.Serialization;

namespace Simplz.ListenToBitcoin.Models;

internal sealed record BaseResponse<T> where T : class, new()
{
    [JsonPropertyName("op")]
    public string Op { get; set; } = string.Empty;

    [JsonPropertyName("x")]
    public T X { get; set; } = new();
}
