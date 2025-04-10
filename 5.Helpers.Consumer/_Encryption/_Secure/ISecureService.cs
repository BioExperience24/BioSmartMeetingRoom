namespace _5.Helpers.Consumer._Encryption._Secure;

public interface ISecureService
{
    string Encrypt(string plaintext);
    string Decrypt(string ciphertext);
}
