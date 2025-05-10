using ActivityUtils.Models;

namespace ActivityUtils.Utils
{
    public class ReminderUtils
    {
        public static DateTime CalcNextRemindDateTime(ReminderContext context)
        {
            DateTime dt = context.ReminderDateTime;
            if (context.Repeating.RepeatType == Enums.ERepeatTypes.HOURLY)
            {
                dt = dt.AddHours(context.Repeating.RepeatInterval);
            }
            else if (context.Repeating.RepeatType == Enums.ERepeatTypes.DAILY)
            {
                dt = dt.AddDays(context.Repeating.RepeatInterval);
            }
            else if (context.Repeating.RepeatType == Enums.ERepeatTypes.WEEKLY)
            {
                dt = dt.AddDays(context.Repeating.RepeatInterval * 7);
            }
            else if (context.Repeating.RepeatType == Enums.ERepeatTypes.MONTHLY)
            {
                dt = dt.AddMonths(context.Repeating.RepeatInterval);
            }
            else if (context.Repeating.RepeatType == Enums.ERepeatTypes.YEARLY)
            {
                dt = dt.AddYears(context.Repeating.RepeatInterval);
            }
            return dt;
        }

        public static int GetUniqueReminderID(List<ReminderContext> list)
        {
            int id;
            do
            {
                id = GenerateUniqueID();
            } while (!CheckUniqueReminderID(id, list));
            return id;
        }

        public static bool CheckUniqueReminderID(int id, List<ReminderContext> list)
        {
            foreach (var item in list)
            {
                if (item.Id == id)
                {
                    return false;
                }
            }
            return true;
        }

        private static int GenerateUniqueID()
        {
            int id = new Random().Next(100000, 1000000);
            return id;
        }

        public static int GetUniqueCategoryID(List<CategoryContext> list)
        {
            int id;
            do
            {
                id = GenerateUniqueID();
            } while (!CheckUniqueCategoryID(id, list));
            return id;
        }

        public static bool CheckUniqueCategoryID(int id, List<CategoryContext> list)
        {
            foreach (var item in list)
            {
                if (item.Id == id)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
