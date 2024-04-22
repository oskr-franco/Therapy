
using System.Security.Cryptography;

namespace Therapy.Core.Utils {
  public static class Hasher
  {
    private const int SaltSize = 16; // 128 bit 
    private const int KeySize = 32; // 256 bit
    private const int Iterations = 10000;
    public static string Hash(string Password)
    {
      using (var algorithm = new Rfc2898DeriveBytes(
        password: Password,
        saltSize: SaltSize,
        iterations: Iterations,
        hashAlgorithm: HashAlgorithmName.SHA256))
      {
        var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return  $"{Iterations}.{salt}.{key}";
      }
    }

    public static bool Verify(string hash, string password)
    {
      var parts = hash.Split('.', 3);

      if (parts.Length != 3)
      {
          throw new FormatException("Unexpected hash format. " +
            "Should be formatted as `{iterations}.{salt}.{hash}`");
      }

      var iterations = Convert.ToInt32(parts[0]);
      var salt = Convert.FromBase64String(parts[1]);
      var key = Convert.FromBase64String(parts[2]);

      var needsUpgrade = iterations != Iterations;

      if (needsUpgrade)
      {
          throw new InvalidOperationException("Password hash must be upgraded.");
      }

      using (var algorithm = new Rfc2898DeriveBytes(
        password,
        salt,
        iterations,
        HashAlgorithmName.SHA256))
      {
          var keyToCheck = algorithm.GetBytes(KeySize);

          var verified = keyToCheck.SequenceEqual(key);

          return verified;
      }
    }
  }
}