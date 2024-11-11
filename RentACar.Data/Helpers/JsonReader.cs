namespace RentACar.Data.Helpers
{
    public static class JsonReader
    {
        public static string ReadJson(string jsonFileName)
        {
            string solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            string filePath = Path.Combine(solutionRoot, "RentACar.Data", "Seeder", "JSON", jsonFileName);
            string jsonContent = File.ReadAllText(filePath);

            return jsonContent;
        }

    }
}
