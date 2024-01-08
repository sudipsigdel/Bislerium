namespace Bislerium.Data
{
    internal class AppUtils
    {
        public static string GetAppDirectoryPath()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Cafe Data"
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
        
         public static string GetCustomerCoffeePath()
       {
           return Path.Combine(GetAppDirectoryPath(), "customer.json");
       }

       public static string GetOrderListPath()
       {
           return Path.Combine(GetAppDirectoryPath(), "orders.json");
       }

    }
}
