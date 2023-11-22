using System.Text.Json.Serialization;

namespace Simplz.ListenToBitcoin.Models;

[JsonSerializable(typeof(BaseResponse<object>))]
[JsonSerializable(typeof(BaseResponse<Unconfirmed>))]
[JsonSerializable(typeof(BaseResponse<Block>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext { }
