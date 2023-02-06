using System.Security.Cryptography;
using System.Text;

namespace Application.Helper.CustomIdentity
{
    public static class UserManager
    {
        public static string GenerateSalt()
        {
            var bytes = new byte[64];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public static string ComputeHash(byte[] bytesToHash, byte[] salt)
        {
            var byteResult = new Rfc2898DeriveBytes(bytesToHash, salt, 10000);
            return Convert.ToBase64String(byteResult.GetBytes(64));
        }

        public static Tuple<string, string> HashPassword(string password)
        {
            var newSalt = GenerateSalt();
            byte[] bytes = Encoding.ASCII.GetBytes(newSalt);
            var hashedPassword = ComputeHash(Encoding.UTF8.GetBytes(password), bytes);
            return Tuple.Create(hashedPassword, newSalt);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(salt);
            return ComputeHash(Encoding.UTF8.GetBytes(password), bytes);
        }
    }
}