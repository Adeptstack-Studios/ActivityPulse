using ActivityUtils.Data;
using ActivityUtils.Enums;
using ActivityUtils.Utils;
using System.Windows;

namespace ActivityUtils.Models
{
    public class ReminderContext
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime ReminderDateTime { get; set; } = DateTime.Now.AddDays(1);
        public bool DoRepeat { get; set; } = false;
        public RepeatingContext Repeating { get; set; } = new RepeatingContext(ERepeatTypes.NONE, 0, ERepeatDuration.FOREVER, 1, DateTime.Now.AddDays(7));
        public EReminderTypes ReminderTypes { get; set; } = EReminderTypes.NORMAL;
        public int CategoryId { get; set; } = 0;
        public bool IsForced { get; set; } = false;
        public bool IsImportant { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        public DateTime NextRemind { get; set; } = DateTime.Now.AddDays(1);
        public string Source { get; set; } = "";
        public string ReminderDateString
        {
            get
            {
                return $"{NextRemind.ToShortDateString()} {NextRemind.ToShortTimeString()}";
            }
        }
        public Visibility ImportantVisibility
        {
            get { return IsImportant ? Visibility.Visible : Visibility.Collapsed; }
        }

        public ReminderContext(string name, DateTime reminderDateTime, RepeatingContext repeating, EReminderTypes reminderTypes, int categoryId, bool repeat, bool isForced, bool isImportant)
        {
            Id = ReminderUtils.GetUniqueReminderID(DataReminders.LoadReminders());
            Name = name;
            ReminderDateTime = reminderDateTime;
            Repeating = repeating;
            ReminderTypes = reminderTypes;
            CategoryId = categoryId;
            IsForced = isForced;
            IsImportant = isImportant;
            DoRepeat = repeat;
        }

        public ReminderContext(string name, DateTime reminderDateTime, EReminderTypes reminderTypes, int categoryId, bool repeat, bool isForced, bool isImportant)
        {
            Id = ReminderUtils.GetUniqueReminderID(DataReminders.LoadReminders());
            Name = name;
            ReminderDateTime = reminderDateTime;
            ReminderTypes = reminderTypes;
            CategoryId = categoryId;
            IsForced = isForced;
            IsImportant = isImportant;
            DoRepeat = repeat;
        }

        public ReminderContext() { }
    }
}
