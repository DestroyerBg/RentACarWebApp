namespace RentACar.Data.Helpers
{
    public static class JsonReader
    {
        public static string ReadJson(string jsonFileName)
        {
            string solutionRoot = string.Empty;

            try
            { 
                solutionRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            }
            catch (Exception e)
            {
                solutionRoot = AppContext.BaseDirectory;
            }

            string filePath = Path.Combine(solutionRoot, "RentACar.Data", "Seeder", "JSON", jsonFileName);
            string jsonContent = File.ReadAllText(filePath);

            return jsonContent;
        }

    }
}
