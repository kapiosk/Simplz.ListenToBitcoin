using System.Text.Json.Serialization;

namespace Simplz.ListenToBitcoin.Models;

internal sealed record Unconfirmed
{
    [JsonPropertyName("lock_time")]
    public int LockTime { get; set; }

    [JsonPropertyName("ver")]
    public int Ver { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("inputs")]
    public List<Input>? Inputs { get; set; }

    [JsonPropertyName("time")]
    public int Time { get; set; }

    [JsonPropertyName("tx_index")]
    public int TxIndex { get; set; }

    [JsonPropertyName("vin_sz")]
    public int VinSz { get; set; }

    [JsonPropertyName("hash")]
    public string? Hash { get; set; }

    [JsonPropertyName("vout_sz")]
    public int VoutSz { get; set; }

    [JsonPropertyName("relayed_by")]
    public string? RelayedBy { get; set; }

    [JsonPropertyName("out")]
    public List<Out>? Out { get; set; }
}
