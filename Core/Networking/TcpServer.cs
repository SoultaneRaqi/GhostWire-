using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GhostWire.Core.Networking;

public class TcpServer
{
    private readonly TcpListener _listener;

    public TcpServer(int port)
    {
        _listener = new TcpListener(IPAddress.Any, port);
    }

    public async Task StartAsync()
    {
        _listener.Start();

        Console.WriteLine($"Listening on port...");

        while (true)
        {
            var client = await _listener.AcceptTcpClientAsync();

            _ = HandleClient(client);
        }
    }

    private async Task HandleClient(TcpClient client)
    {
        using var stream = client.GetStream();

        var buffer = new byte[4096];

        int bytesRead = await stream.ReadAsync(buffer);

        var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

        Console.WriteLine($"Received: {message}");
    }
}