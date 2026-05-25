using GhostWire.Core.Networking;

var server = new TcpServer(7777);

_ = server.StartAsync();

Console.WriteLine("Press ENTER to send test message");
Console.ReadLine();

var client = new TcpClientService();

await client.SendAsync(
    "127.0.0.1",
    7777,
    "hello from ghostwire"
);

Console.ReadLine();