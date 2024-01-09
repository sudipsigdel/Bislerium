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
    }
}
