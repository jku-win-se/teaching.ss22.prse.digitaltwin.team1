namespace SmartRoom.CommonBase.Utils.Contracts
{
    public interface IAes256Cipher
    {
        string Decrypt(string value);
        string Encrypt(string value);
    }
}