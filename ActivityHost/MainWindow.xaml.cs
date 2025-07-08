using ActivityUtils.Data;
using ActivityUtils.Models;
using ActivityUtils.Utils;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
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

        List<ReminderContext> reminders = new List<ReminderContext>();

        List<AppUsage> appUsages = new List<AppUsage>();
        GeneralData generalData = new GeneralData();
        string storeFolder = @$"{DataTracker.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
        string storeFileApps = @$"{DataTracker.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Day}.json";
        string storeFileGeneral = @$"{DataTracker.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/General.json";
        DateTime currentDate = DateTime.Now;
        const int evaluationTime = 15;

        public MainWindow()
        {
            InitializeComponent();
            EnableAutostart();
            this.Hide();
            DataTracker.Create();
            DataTracker.CreateFolder(storeFolder);
            DataTracker.CreateFile(storeFileApps);
            DataTracker.CreateFile(storeFileGeneral);
            appUsages = DataTracker.LoadList(storeFileApps);
            generalData = DataTracker.LoadGeneralData(storeFileGeneral);
            Timer();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
        }

        void EnableAutostart()
        {
            string path = Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName);
            RegistryKey? rk = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rk?.GetValue("ActivityHost") == null)
            {
                rk?.SetValue("ActivityHost", path);
            }
        }

        void Update()
        {
            if (currentDate.Day != DateTime.Now.Day)
            {
                DataTracker.Save(storeFileApps, storeFileGeneral, appUsages, generalData);
                appUsages.Clear();
                generalData = new GeneralData();
                currentDate = DateTime.Now;

                storeFolder = @$"{DataTracker.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}";
                storeFileApps = @$"{DataTracker.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Day}.json";
                storeFileGeneral = @$"{DataTracker.path}{DateTime.Now.Year}/{DateTime.Now.Month}/{DateTime.Now.Day}/General.json";
                DataTracker.CreateFolder(storeFolder);
                DataTracker.CreateFile(storeFileApps);
                DataTracker.CreateFile(storeFileGeneral);
            }

        }

        public void CheckReminder()
        {
            reminders.Clear();
            reminders = DataReminders.LoadReminders();

            for (int i = 0; i < reminders.Count; i++)
            {
                if (i < reminders.Count)
                {
                    if (reminders[i].NextRemind.Date == DateTime.Now.Date && reminders[i].NextRemind.ToShortTimeString() == DateTime.Now.ToShortTimeString() && !reminders[i].IsCompleted)
                    {
                        ReminderContext r = reminders[i];
                        Thread t = new Thread(() => AnnounceReminder(r));
                        t.Start();
                    }
                    //MessageBox.Show(i.ToString());
                }
            }
        }

        public void AnnounceReminder(ReminderContext r)
        {
            Dispatcher.Invoke(() =>
            {
                ReminderWindow rw = new ReminderWindow(r);
                Nullable<bool> result = rw.ShowDialog();
                MessageBox.Show(result.ToString());
                if (result == true)
                {
                    if (r.DoRepeat)
                    {
                        r.DayRepeatCount = 0;
                        ReminderContext rc = ReminderUtils.CalcRepeat(r);
                        r.Repeating = rc.Repeating;
                        r.NextRemind = rc.NextRemind;
                        r.IsCompleted = rc.IsCompleted;
                    }
                    else
                    {
                        r.IsCompleted = true;
                    }
                }
                else
                {
                    MessageBox.Show(r.DayRepeatCount + " 1");
                    r.DayRepeatCount += 1;
                    MessageBox.Show(r.DayRepeatCount + " 2");
                    MessageBox.Show(r.NextRemind + " 1.1");
                    r.NextRemind = r.NextRemind.AddMinutes(15);
                    MessageBox.Show(r.NextRemind + " 2.1");
                }
                List<ReminderContext> rmd = DataReminders.LoadReminders();
                int i = rmd.FindIndex(o => o.Id == r.Id);
                rmd[i] = r;
                DataReminders.SaveReminder(rmd);
            });
        }

        (string name, string icon) GetActiveProcessData()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            if (!Utilities.IsBannedProcess(p))
            {
                try
                {
                    return (Path.GetFileNameWithoutExtension(p.MainModule.FileName), GetIcon(p.MainModule.FileName));//.Dump();
                }
                catch (Exception)
                {
                    return ("Other", "");
                }
            }
            return ("Other", "");
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
                    CheckReminder();
                    offTimer = evaluationTime;
                    CaptureGeneralData();
                    CaptureAppData();
                    DataTracker.Save(storeFileApps, storeFileGeneral, appUsages, generalData);
                }
            });
            timer.Start();
        }

        void CaptureGeneralData()
        {
            bool allow = true;
            foreach (var item in generalData.timeUsed)
            {
                if (item.Minute == DateTime.Now.Minute && item.Hour == DateTime.Now.Hour)
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
            var data = GetActiveProcessData();
            if (data.name != "Other" && !string.IsNullOrEmpty(data.icon))
            {
                string app = data.name;
                string icon = data.icon;
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
                        if (item.Minute == DateTime.Now.Minute && item.Hour == DateTime.Now.Hour)
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
                        IconPath = icon,
                    });
                }
            }
        }

        public string GetIcon(string fileName)
        {
            string path = Path.Combine(DataTracker.path, "AppIcons");
            string file = Path.Combine(path, Path.GetFileNameWithoutExtension(fileName) + ".png");
            if (!File.Exists(file))
            {
                Icon? icon = System.Drawing.Icon.ExtractAssociatedIcon(fileName);
                Bitmap bmp = icon.ToBitmap();
                bmp.Save(file, System.Drawing.Imaging.ImageFormat.Png);
            }
            return file;
        }
    }
}