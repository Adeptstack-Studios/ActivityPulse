using ActivityUtilities.Models;
using System.Text.Json;

namespace ActivityUtilities.Data
{
    public class DataReminders
    {
        static string categoriesPath = $"{DataTracker.path}/Reminder/categories.json";
        static string reminderPath = $"{DataTracker.path}/Reminder/reminder.json";

        public static void Save(List<CategoryContext> categories, List<ReminderContext> reminder)
        {
            if (!File.Exists(categoriesPath))
            {
                File.Create(categoriesPath).Dispose();
            }
            if (!File.Exists(reminderPath))
            {
                File.Create(reminderPath).Dispose();
            }

            File.WriteAllText($"{DataTracker.path}/Reminder/categories.json", JsonSerializer.Serialize(categories));
            File.WriteAllText($"{DataTracker.path}/Reminder/reminder.json", JsonSerializer.Serialize(reminder));
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
                Console.WriteLine(e.Message);
            }
            return new();
        }
    }
}
