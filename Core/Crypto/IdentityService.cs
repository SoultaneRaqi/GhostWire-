using Sodium;

namespace GhostWire.Core.Crypto;

public class IdentityService
{
    public byte[] PublicKey { get; private set; }
    public byte[] PrivateKey { get; private set; }

    public IdentityService()
    {
        // Changed to generate Key Exchange (Curve25519) keys for encryption
        var keyPair = PublicKeyBox.GenerateKeyPair();

        PublicKey = keyPair.PublicKey;
        PrivateKey = keyPair.PrivateKey;
    }

    public string GetFingerprint()
    {
        return Convert.ToHexString(PublicKey)[..16];
    }
}