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
    }
}
