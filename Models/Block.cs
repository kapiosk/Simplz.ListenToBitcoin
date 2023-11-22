using System.Text.Json.Serialization;

namespace Simplz.ListenToBitcoin.Models;

internal sealed record Block
{
    [JsonPropertyName("txIndexes")]
    public List<int>? TxIndexes { get; set; }

    [JsonPropertyName("nTx")]
    public int NTx { get; set; }

    [JsonPropertyName("totalBTCSent")]
    public int TotalBTCSent { get; set; }

    [JsonPropertyName("estimatedBTCSent")]
    public int EstimatedBTCSent { get; set; }

    [JsonPropertyName("reward")]
    public int Reward { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("blockIndex")]
    public int BlockIndex { get; set; }

    [JsonPropertyName("prevBlockIndex")]
    public int PrevBlockIndex { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("hash")]
    public string? Hash { get; set; }

    [JsonPropertyName("mrklRoot")]
    public string? MrklRoot { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("time")]
    public int Time { get; set; }

    [JsonPropertyName("bits")]
    public int Bits { get; set; }

    [JsonPropertyName("nonce")]
    public int Nonce { get; set; }
}
