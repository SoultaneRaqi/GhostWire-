using GhostWire.Core.Crypto;

Console.WriteLine("--- GhostWire E2E Test ---");

// 1. Generate two unique identities
var nodeA = new IdentityService();
var nodeB = new IdentityService();

Console.WriteLine($"Node A Fingerprint: {nodeA.GetFingerprint()}");
Console.WriteLine($"Node B Fingerprint: {nodeB.GetFingerprint()}\n");

// 2. Node A encrypts a message intended for Node B
string originalMessage = "Top secret mesh network data!";
Console.WriteLine($"[Node A] Original: {originalMessage}");

byte[] encryptedPayload = EncryptionService.Encrypt(
    originalMessage, 
    nodeB.PublicKey,   // Node A needs Node B's public key to lock it
    nodeA.PrivateKey   // Node A uses its own private key to sign it
);

Console.WriteLine($"[Network] Payload moving through network: {Convert.ToBase64String(encryptedPayload)[..40]}... (Unreadable)");

// 3. Node B receives the payload and decrypts it
string decryptedMessage = EncryptionService.Decrypt(
    encryptedPayload,
    nodeA.PublicKey,   // Node B needs Node A's public key to verify who sent it
    nodeB.PrivateKey   // Node B uses its own private key to unlock it
);

Console.WriteLine($"[Node B] Decrypted: {decryptedMessage}");