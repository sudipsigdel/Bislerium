using System.Text.Json;


namespace Bislerium.Data
{
    public class AddinsServices
    {
        private readonly List<Addins> _addinsList = new()
        {
            new() { AddinsType = "Sugar", Price = 20 },
            new() { AddinsType = "Milk", Price = 25 },
            new() { AddinsType = "Flavored Syrup", Price = 30 },
            new() { AddinsType = "Cinnamon", Price = 15 },
            new() { AddinsType = "Cocoa Powder", Price = 25 },
            new() { AddinsType = "Whipped Cream", Price = 40 },
            new() { AddinsType = "Condensed Milk", Price = 35 },
            new() { AddinsType = "Butter", Price = 30 },
            new() { AddinsType = "Coconut Oil", Price = 25 },
            new() { AddinsType = "Nutmeg", Price = 10 }
        };

        public void SaveAddinsInJsonFile(List<Addins> addinsList)
        {
            string appDataDirPath = AppUtils.GetAppDirectoryPath();
            string addinsListFilePath = AppUtils.GetAddinsFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(addinsList);

            File.WriteAllText(addinsListFilePath, json);
        }

        public List<Addins> GetAddinsFromJsonFile()
        {
            string addinsListFilePath = AppUtils.GetAddinsFilePath();

            if (!File.Exists(addinsListFilePath))
            {
                return new List<Addins>();
            }

            var json = File.ReadAllText(addinsListFilePath);

            return JsonSerializer.Deserialize<List<Addins>>(json);
        }

        //This Function create a list of JSON Addins List in the Computer just for one timne
        public void SeedAddinsDetails()
        {
            List<Addins> addinsList = GetAddinsFromJsonFile();

            if (addinsList.Count == 0)
            {
                SaveAddinsInJsonFile(_addinsList);
            }
        }

        public Addins GetAddinsDetailsByID(string addinsID)
        {
            List<Addins> addinsList = GetAddinsFromJsonFile();
            Addins addins = addinsList.FirstOrDefault(addins => addins.Id.ToString() == addinsID);
            return addins;
        }

        public void UpdateAddinsDetails(Addins addins)
        {
            List<Addins> addinsList = GetAddinsFromJsonFile();

            Addins addinsToUpdate = addinsList.FirstOrDefault(_addins => _addins.Id.ToString() == addins.Id.ToString());

            if (addinsToUpdate == null)
            {
                throw new Exception("Addins not found");
            }

            addinsToUpdate.AddinsType = addins.AddinsType;
            addinsToUpdate.Price = addins.Price;

            SaveAddinsInJsonFile(addinsList);
        }

    }
}
