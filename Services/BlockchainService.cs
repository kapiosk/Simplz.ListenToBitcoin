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
            client.Send(JsonSerializer.Serialize(new { op = "unconfirmed_sub" }));
            client.Send(JsonSerializer.Serialize(new { op = "blocks_sub" }));
        });
        await client.Start();

        client.MessageReceived.Subscribe(msg =>
        {
            if (msg.MessageType == WebSocketMessageType.Text && msg.Text is not null)
                try
                {
                    var message = JsonSerializer.Deserialize<BaseResponse<object>>(msg.Text);
                    var text = message?.X?.ToString();
                    if (message?.Op is not null && text is not null)
                        switch (message.Op)
                        {
                            case "utx":
                                var unconfirmed = JsonSerializer.Deserialize<Unconfirmed>(text);
                                if (unconfirmed is not null)
                                    Console.WriteLine($"Unconfirmed: {unconfirmed.Hash}");
                                break;
                            case "block":
                                var block = JsonSerializer.Deserialize<Block>(text);
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
//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         using var ws = new ClientWebSocket();
//         await ws.ConnectAsync(new Uri("wss://ws.blockchain.info/inv"), stoppingToken);
//         await ws.SendAsync(Encoding.UTF8.GetBytes("{\"op\":\"unconfirmed_sub\"}"), WebSocketMessageType.Text, true, stoppingToken);
//         await ws.SendAsync(Encoding.UTF8.GetBytes("{\"op\":\"blocks_sub\"}"), WebSocketMessageType.Text, true, stoppingToken);
//         while (ws.State == WebSocketState.Open && !stoppingToken.IsCancellationRequested)
//         {
//             var arraySegment = new ArraySegment<byte>(new byte[8192]);
//             var receivedMessage = await ws.ReceiveAsync(arraySegment, stoppingToken);
//             if (receivedMessage.MessageType == WebSocketMessageType.Text)
//             {
//                 var txt = Encoding.Default.GetString(arraySegment).TrimEnd('\0');
//                 try
//                 {
//                     var message = JsonSerializer.Deserialize<BaseResponse<object>>(txt);
//                     if (message?.X is not null)
//                         switch (message.Op)
//                         {
//                             case "utx":
//                                 var unconfirmed = JsonSerializer.Deserialize<Unconfirmed>(message.X.ToString() ?? "");
//                                 if (unconfirmed is not null)
//                                     Console.WriteLine($"Unconfirmed: {unconfirmed.Hash}");
//                                 break;
//                             case "block":
//                                 var block = JsonSerializer.Deserialize<Block>(message.X.ToString() ?? "");
//                                 if (block is not null)
//                                     Console.WriteLine($"Block: {block.Hash}");
//                                 break;
//                         }
//                 }
//                 catch (Exception ex)
//                 {
//                     Console.WriteLine(ex.Message);
//                 }
//             }
//         }
//     }
// }
