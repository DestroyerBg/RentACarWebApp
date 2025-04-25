namespace RentACar.Data.Helpers
{
    public static class JsonReader
    {
        public static string ReadJson(string jsonFileName)
        {
            string filePath;

            filePath = Path.Combine(AppContext.BaseDirectory, jsonFileName);
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }

            string localJsonPath = Path.Combine(
                Directory.GetCurrentDirectory(), 
                "RentACar.Data", "Seeder", "JSON", jsonFileName
            );
            if (File.Exists(localJsonPath))
            {
                return File.ReadAllText(localJsonPath);
            }

            throw new FileNotFoundException(
                $"JSON файлът '{jsonFileName}' не беше намерен нито в wwwroot, нито в локалния проект.");
        }

    }
}
