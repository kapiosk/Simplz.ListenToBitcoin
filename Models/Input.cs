using System.Text.Json.Serialization;

namespace Simplz.ListenToBitcoin.Models;

internal sealed record Input
{
    [JsonPropertyName("sequence")]
    public long Sequence { get; set; }

    [JsonPropertyName("prev_out")]
    public PrevOut? PrevOut { get; set; }

    [JsonPropertyName("script")]
    public string? Script { get; set; }
}
