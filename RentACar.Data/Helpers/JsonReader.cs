namespace RentACar.Data.Helpers
{
    public static class JsonReader
    {
        public static string ReadJson(string jsonFileName)
        {
            string solutionRoot = string.Empty;
            string filePath = string.Empty;
            string jsonContent = string.Empty;
            try
            { 
                solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            }
            catch (Exception e)
            {
                solutionRoot = AppContext.BaseDirectory;
                filePath = Path.Combine(solutionRoot, jsonFileName);
                jsonContent = File.ReadAllText(filePath);
                return jsonContent;
            }

            filePath = Path.Combine(solutionRoot, "RentACar.Data", "Seeder", "JSON", jsonFileName);
            jsonContent = File.ReadAllText(filePath);

            return jsonContent;
        }

    }
}
