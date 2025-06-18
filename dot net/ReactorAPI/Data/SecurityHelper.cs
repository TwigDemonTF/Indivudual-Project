using System.Security.Cryptography;
using System.Text;

public static class SecurityHelper
{
    public static byte[] GenerateSalt(int size = 16)
    {
        var salt = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }

    public static byte[] HashPassword(string password, byte[] salt, int iterations = 100_000, int hashByteSize = 32)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(hashByteSize);
    }

    public static bool VerifyPassword(string enteredPassword, byte[] storedHash, byte[] storedSalt)
    {
        var enteredHash = HashPassword(enteredPassword, storedSalt);
        return storedHash.SequenceEqual(enteredHash);
    }
}