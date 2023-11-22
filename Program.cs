using Simplz.ListenToBitcoin.Models;
using Simplz.ListenToBitcoin.Services;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddHostedService<BlockchainService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.RunAsync();
