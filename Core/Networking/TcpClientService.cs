using System.Net.Sockets;
using System.Text;

namespace GhostWire.Core.Networking;

public class TcpClientService
{
    public async Task SendAsync(string host, int port, string message)
    {
        using var client = new TcpClient();

        await client.ConnectAsync(host, port);

        using var stream = client.GetStream();

        var data = Encoding.UTF8.GetBytes(message);

        await stream.WriteAsync(data);
    }
}