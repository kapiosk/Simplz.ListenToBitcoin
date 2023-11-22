using System.Net.WebSockets;
using System.Text.Json;
using Simplz.ListenToBitcoin.Models;
using Websocket.Client;

namespace Simplz.ListenToBitcoin.Services;
//100 million satoshis in a bitcoin
internal sealed class BlockchainService : BackgroundService
{
    public BlockchainService() { }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var client = new WebsocketClient(new("wss://ws.blockchain.info/inv"));
        client.ReconnectTimeout = TimeSpan.FromSeconds(30);
        client.ReconnectionHappened.Subscribe(info =>
        {
            client.Send(JsonSerializer.Serialize(new BaseResponse<object> { Op = "unconfirmed_sub" }, AppJsonSerializerContext.Default.BaseResponseObject));
            client.Send(JsonSerializer.Serialize(new BaseResponse<object> { Op = "blocks_sub" }, AppJsonSerializerContext.Default.BaseResponseObject));
        });
        await client.Start();

        client.MessageReceived.Subscribe(msg =>
        {
            if (msg.MessageType == WebSocketMessageType.Text && msg.Text is not null)
                try
                {
                    var message = JsonSerializer.Deserialize(msg.Text, AppJsonSerializerContext.Default.BaseResponseObject);
                    var text = message?.X.ToString();
                    if (text is not null)
                        switch (message?.Op)
                        {
                            case "utx":
                                var unconfirmed = JsonSerializer.Deserialize(text, AppJsonSerializerContext.Default.Unconfirmed);
                                if (unconfirmed is not null)
                                    Console.WriteLine($"Unconfirmed: {unconfirmed.Hash}");
                                break;
                            case "block":
                                var block = JsonSerializer.Deserialize(text, AppJsonSerializerContext.Default.Block);
                                if (block is not null)
                                    Console.WriteLine($"Block: {block.Hash}");
                                break;
                        }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
        });
        stoppingToken.WaitHandle.WaitOne();
    }
}
