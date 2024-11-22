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

            string icons = Path.Combine(path, "AppIcons");
            if (!Directory.Exists(icons))
            {
                Directory.CreateDirectory(icons);
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

        public static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            DirectoryInfo[] dirs = dir.GetDirectories();
            Directory.CreateDirectory(destinationDir);

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                try
                {
                    file.CopyTo(targetFilePath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }
    }
}
