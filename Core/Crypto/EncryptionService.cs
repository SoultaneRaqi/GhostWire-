using Sodium;
using System.Text;

namespace GhostWire.Core.Crypto;

public class EncryptionService
{
    // Encrypts a message using Sender's Private Key and Receiver's Public Key
    public static byte[] Encrypt(string message, byte[] receiverPublicKey, byte[] senderPrivateKey)
    {
        var nonce = PublicKeyBox.GenerateNonce();
        var messageBytes = Encoding.UTF8.GetBytes(message);

        // Creates the encrypted ciphertext
        var cipherText = PublicKeyBox.Create(messageBytes, nonce, senderPrivateKey, receiverPublicKey);

        // Prepend the 24-byte nonce to the ciphertext so the receiver can use it
        var payload = new byte[nonce.Length + cipherText.Length];
        Buffer.BlockCopy(nonce, 0, payload, 0, nonce.Length);
        Buffer.BlockCopy(cipherText, 0, payload, nonce.Length, cipherText.Length);

        return payload;
    }

    // Decrypts a payload using Receiver's Private Key and Sender's Public Key
    public static string Decrypt(byte[] payload, byte[] senderPublicKey, byte[] receiverPrivateKey)
    {
        // Extract the 24-byte nonce from the beginning
        var nonce = new byte[24];
        Buffer.BlockCopy(payload, 0, nonce, 0, nonce.Length);

        // Extract the actual ciphertext from the rest of the payload
        var cipherText = new byte[payload.Length - nonce.Length];
        Buffer.BlockCopy(payload, nonce.Length, cipherText, 0, cipherText.Length);

        // Open the box (Decrypt)
        var decryptedBytes = PublicKeyBox.Open(cipherText, nonce, receiverPrivateKey, senderPublicKey);

        return Encoding.UTF8.GetString(decryptedBytes);
    }
}