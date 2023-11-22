using System.Text.Json.Serialization;

namespace Simplz.ListenToBitcoin.Models;

internal sealed record PrevOut
{
    [JsonPropertyName("spent")]
    public bool Spent { get; set; }

    [JsonPropertyName("tx_index")]
    public int TxIndex { get; set; }

    [JsonPropertyName("type")]
    public int Type { get; set; }

    [JsonPropertyName("addr")]
    public string? Addr { get; set; }

    [JsonPropertyName("value")]
    public long Value { get; set; }

    [JsonPropertyName("n")]
    public int N { get; set; }

    [JsonPropertyName("script")]
    public string? Script { get; set; }
}
