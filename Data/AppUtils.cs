using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium.Data
{
    internal class AppUtils
    {

        public static string GetDesktopDirectoryPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }


        public static string GetCofeeListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "coffeeList.json");
        }

        public static string GetCustomerCoffeePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "customer.json");
        }
        //getting the JSON Path for Adins
        public static string GetAddInItemListPath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "Addins.json");
        }
        public static string GetOrderListPath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "orders.json");
        }

    }
}
