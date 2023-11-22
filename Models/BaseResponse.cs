using System.Text.Json.Serialization;

namespace Simplz.ListenToBitcoin.Models;

internal sealed record BaseResponse<T> where T : class
{
    [JsonPropertyName("op")]
    public string? Op { get; set; }

    [JsonPropertyName("x")]
    public T? X { get; set; }
}
