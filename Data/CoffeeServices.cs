using System.Text.Json;

namespace Bislerium.Data
{
    public class CoffeeServices
    {
        private readonly List<Coffee> _coffeeList = new()
        {
            new() { CoffeeType = "Espresso", Price = 200},
            new() { CoffeeType = "Americano", Price = 185},
            new() { CoffeeType = "Latte", Price = 210},
            new() { CoffeeType = "Cappuccino", Price = 250},
            new() { CoffeeType = "Mocha", Price = 230},
            new() { CoffeeType = "Cold Brew", Price = 205},
            new() { CoffeeType = "Iced Coffee", Price = 245},
            new() { CoffeeType = "Café au Lait", Price = 360},
            new() { CoffeeType = "Café Cubano", Price = 275 },
            new() { CoffeeType = "Vienna Coffee", Price = 270 }
        };

        public void SaveCoffeeInJsonFile(List<Coffee> coffeeList)
        {
            string appDataDirPath = AppUtils.GetAppDirectoryPath();
            string coffeeListFilePath = AppUtils.GetCoffeeFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(coffeeList);

            File.WriteAllText(coffeeListFilePath, json);
        }

        public List<Coffee> GetCoffeeFromJsonFile()
        {
            string coffeeListFilePath = AppUtils.GetCoffeeFilePath();

            if (!File.Exists(coffeeListFilePath))
            {
                return new List<Coffee>();
            }

            var json = File.ReadAllText(coffeeListFilePath);

            return JsonSerializer.Deserialize<List<Coffee>>(json);
        }

        //This Function create a list of JSON Coffee List in the Computer just for one timne
        public void SeedCoffeeDetails()
        {
            List<Coffee> coffeeList = GetCoffeeFromJsonFile();

            if (coffeeList.Count == 0)
            {
                SaveCoffeeInJsonFile(_coffeeList);
            }
        }

        public Coffee GetCoffeeDetailsByID(String coffeeID)
        {
            List<Coffee> coffeeList = GetCoffeeFromJsonFile();
            Coffee coffee = coffeeList.FirstOrDefault(coffee => coffee.Id.ToString() == coffeeID);
            return coffee;
        }

        public void UpdateCoffeeDetails(Coffee coffee)
        {
            List<Coffee> coffeeList = GetCoffeeFromJsonFile();

            Coffee coffeeToUpdate = coffeeList.FirstOrDefault(_coffee => _coffee.Id.ToString() == coffee.Id.ToString());

            if (coffeeToUpdate == null)
            {
                throw new Exception("Coffee not found");
            }

            coffeeToUpdate.CoffeeType = coffee.CoffeeType;
            coffeeToUpdate.Price = coffee.Price;

            SaveCoffeeInJsonFile(coffeeList);
        }

    }
}
