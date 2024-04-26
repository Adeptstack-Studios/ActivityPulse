using System.Text.Json;

namespace ActivityUtilities
{
    public class Data
    {
        public static string path = @$"C://Users/{Environment.UserName}/Documents/ActivityPulse/";

        public static void Create()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void CreateFile(string file)
        {
            if (!File.Exists(file))
            {
                File.Create(file).Dispose();
            }
        }

        public static void CreateFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public static void Save(string appFilePath, string generalFilePath, List<AppUsage> appUsages, GeneralData generalData)
        {
            File.WriteAllText(appFilePath, JsonSerializer.Serialize(appUsages));
            File.WriteAllText(generalFilePath, JsonSerializer.Serialize(generalData));
        }

        public static List<AppUsage> LoadList(string appFilePath)
        {
            try
            {
                var contentApp = File.ReadAllText(appFilePath);
                return JsonSerializer.Deserialize<List<AppUsage>>(contentApp) ?? new();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new();
        }

        public static GeneralData LoadGeneralData(string generalFilePath)
        {
            try
            {
                var contentGeneral = File.ReadAllText(generalFilePath);
                return JsonSerializer.Deserialize<GeneralData>(contentGeneral) ?? new();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new();
        }
    }
}
