using System.IO;

namespace ActivityHost
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
    }
}
