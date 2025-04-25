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

            string solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            filePath = Path.Combine(solutionRoot, "RentACar.Data", "Seeder", "JSON", jsonFileName);

            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }

            throw new FileNotFoundException(
                $"JSON файлът '{jsonFileName}' не беше намерен нито в wwwroot, нито в локалния проект.");
        }

    }
}
