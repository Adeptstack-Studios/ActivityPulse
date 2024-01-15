using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;

namespace ActivityHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        List<AppUsage> appUsages = new List<AppUsage>();
        GeneralData generalData = new GeneralData();
        string storeFolder = @$"{Data.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
        string storeFileApps = @$"{Data.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Day}.json";
        string storeFileGeneral = @$"{Data.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/General.json";
        DateTime currentDate = DateTime.Now;
        const int evaluationTime = 15;

        public MainWindow()
        {
            InitializeComponent();
            EnableAutostart();
            this.Hide();
            Data.Create();
            Data.CreateFolder(storeFolder);
            Data.CreateFile(storeFileApps);
            Data.CreateFile(storeFileGeneral);
            Load();
            Timer();
        }

        void EnableAutostart()
        {
            string path = Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName);
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rk.GetValue("ActivityHost") == null)
            {
                rk.SetValue("ActivityHost", path);
            }
        }

        void Update()
        {
            if (currentDate.Day != DateTime.Now.Day)
            {
                Save();
                appUsages.Clear();
                generalData = new GeneralData();
                currentDate = DateTime.Now;

                storeFolder = @$"{Data.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
                storeFileApps = @$"{Data.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Day}.json";
                storeFileGeneral = @$"{Data.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/General.json";
                Data.CreateFolder(storeFolder);
                Data.CreateFile(storeFileApps);
                Data.CreateFile(storeFileGeneral);
            }

        }

        string GetActiveProcessFileName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            try
            {
                return Path.GetFileNameWithoutExtension(p.MainModule.FileName);//.Dump();
            }
            catch (Exception)
            {
                return "Other";
            }
        }

        int offTimer = evaluationTime;
        private void Timer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);

            timer.Tick += new EventHandler(delegate (object s, EventArgs a)
            {
                Update();
                offTimer--;
                if (offTimer == 0)
                {
                    offTimer = evaluationTime;
                    CaptureGeneralData();
                    CaptureAppData();
                    Save();
                }
            });
            timer.Start();
        }

        void CaptureGeneralData()
        {
            bool allow = true;
            foreach (var item in generalData.timeUsed)
            {
                if (item.Minute == DateTime.Now.Minute)
                {
                    allow = false;
                    break;
                }
            }

            if (allow)
            {
                generalData.timeUsed.Add(DateTime.Now);
            }

            generalData.gesSecondsUsed += evaluationTime;
        }

        void CaptureAppData()
        {
            string app = GetActiveProcessFileName();
            int index = 0;
            bool exists = false;

            for (int i = 0; i < appUsages.Count; i++)
            {
                if (appUsages[i].AppName == app)
                {
                    index = i;
                    exists = true;
                    break;
                }
            }

            if (exists)
            {
                appUsages[index].UsedSeconds += evaluationTime;
                if (appUsages[index].UsedSeconds >= 60)
                {
                    appUsages[index].UsedMinutes += 1;
                    appUsages[index].UsedSeconds = appUsages[index].UsedSeconds - 60;
                }

                bool allow = true;
                foreach (var item in appUsages[index].timeUsed)
                {
                    if (item.Minute == DateTime.Now.Minute)
                    {
                        allow = false;
                        break;
                    }
                }

                if (allow)
                {
                    appUsages[index].timeUsed.Add(DateTime.Now);
                }
            }
            else
            {
                appUsages.Insert(0, new AppUsage
                {
                    AppName = app,
                    UsedSeconds = 1,
                    UsedMinutes = 0,
                });
            }
        }

        void Save()
        {
            File.WriteAllText(storeFileApps, JsonSerializer.Serialize(appUsages));
            File.WriteAllText(storeFileGeneral, JsonSerializer.Serialize(generalData));
        }

        void Load()
        {
            try
            {
                var contentApp = File.ReadAllText(storeFileApps);
                appUsages = JsonSerializer.Deserialize<List<AppUsage>>(contentApp) ?? new();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                var contentGeneral = File.ReadAllText(storeFileGeneral);
                generalData = JsonSerializer.Deserialize<GeneralData>(contentGeneral) ?? new();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}