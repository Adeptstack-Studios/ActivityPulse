using ActivityUtils.Models;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace ActivityUtils.Data
{
    public class DataReminders
    {
        static string categoriesPath = $"{DataTracker.path}/Reminder/categories.json";
        static string reminderPath = $"{DataTracker.path}/Reminder/reminder.json";
        static string completedReminderPath = $"{DataTracker.path}/Reminder/completedReminder.json";

        public static void SaveReminder(List<ReminderContext> reminder)
        {
            if (!File.Exists(reminderPath))
            {
                File.Create(reminderPath).Dispose();
            }

            File.WriteAllText($"{DataTracker.path}/Reminder/reminder.json", JsonSerializer.Serialize(reminder));
        }

        public static void SaveCompletedReminder(List<ReminderContext> reminder)
        {
            if (!File.Exists(completedReminderPath))
            {
                File.Create(reminderPath).Dispose();
            }

            File.WriteAllText(completedReminderPath, JsonSerializer.Serialize(reminder));
        }

        public static void SaveCategories(List<CategoryContext> categories)
        {
            if (!File.Exists(categoriesPath))
            {
                File.Create(categoriesPath).Dispose();
            }
            File.WriteAllText($"{DataTracker.path}/Reminder/categories.json", JsonSerializer.Serialize(categories));
        }

        public static List<CategoryContext> LoadCategories()
        {
            try
            {
                var contentApp = File.ReadAllText(categoriesPath);
                return JsonSerializer.Deserialize<List<CategoryContext>>(contentApp) ?? new();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new();
        }

        public static List<ReminderContext> LoadReminders()
        {
            try
            {
                var contentGeneral = File.ReadAllText(reminderPath);
                return JsonSerializer.Deserialize<List<ReminderContext>>(contentGeneral) ?? new();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
            return new();
        }

        public static List<ReminderContext> LoadCompletedReminders()
        {
            try
            {
                var contentGeneral = File.ReadAllText(completedReminderPath);
                return JsonSerializer.Deserialize<List<ReminderContext>>(contentGeneral) ?? new();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                MessageBox.Show(e.ToString());
            }
            return new();
        }
    }
}
