using System.Security.Cryptography;

namespace Bislerium.Data
{
    internal class AppUtils
    {
        public static string GetAppDirectoryPath()
        {
            string customPath = @"C:\Users\Acer\OneDrive\Desktop\Bislerium\";
            
            return Path.Combine(
               customPath, "Bislerium Cafe Data"
            );
        }
        public static string GetCoffeeFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "coffee.json");
        }
        public static string GetAddinsFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "addins.json");
        }
        public static string GetCustomerPath()
        {
            return Path.Combine(GetAppDirectoryPath(), "customer.json");
        }
       public static string GetOrderListPath()
       {
           return Path.Combine(GetAppDirectoryPath(), "orders.json");
       }
       public static string GetOrderItemListPath()
       {
           return Path.Combine(GetAppDirectoryPath(), "orderItem.json");
       }

        //Password Hashing
        private const char _segmentDelimiter = ':';

        public static string HashSecret(string input)
        {
            var saltSize = 16;
            var iterations = 100_000;
            var keySize = 32;
            HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algorithm, keySize);

            return string.Join(
                _segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                iterations,
                algorithm
            );
        }

        public static bool VerifyHash(string input, string hashString)
        {
            string[] segments = hashString.Split(_segmentDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );

            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }
    }
}
