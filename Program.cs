using GhostWire.Core.Crypto;
using GhostWire.Core.Networking;

// 1. Setup Node Identity
var identity = new IdentityService();
int randomTcpPort = new Random().Next(7000, 8000); 

Console.Write("Enter your alias: ");
var alias = Console.ReadLine() ?? "Unknown";

Console.Clear();
Console.WriteLine("=== GhostWire Node ===");
Console.WriteLine($"Alias:       {alias}");
Console.WriteLine($"Fingerprint: {identity.GetFingerprint()}");
Console.WriteLine($"TCP Port:    {randomTcpPort}");
Console.WriteLine("======================\n");

// 2. Start Discovery Service
var discovery = new DiscoveryService(identity, alias, randomTcpPort);

// Run listening and broadcasting in the background
_ = discovery.StartListeningAsync();
_ = discovery.StartBroadcastingAsync();

// Keep the app running
Console.WriteLine("Waiting for peers... (Press Ctrl+C to exit)");
await Task.Delay(-1);