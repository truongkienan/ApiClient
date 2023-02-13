using System.Security.Cryptography;
using System.Text;

namespace WebApp
{
    public class Helper
    {
        public static byte[] Hash(string plaintext)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA-512");
            return algorithm.ComputeHash(Encoding.ASCII.GetBytes(plaintext));
        }
        public static string RandomString(int len)
        {
            string pattern = "qwertyuiopasdfghjklzxcvbnm1234567890";
            char[] arr = new char[len];
            Random rand = new Random();
            for (int i = 0; i < len; i++)
            {
                arr[i] = pattern[rand.Next(pattern.Length)];
            }
            return string.Join(string.Empty, arr);
        }
    }
}
