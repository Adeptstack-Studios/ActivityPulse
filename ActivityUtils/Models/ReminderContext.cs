using ActivityUtils.Enums;

namespace ActivityUtils.Models
{
    public class ReminderContext
    {
        public Guid Id { get; }
        public string Name { get; set; } = "";
        public DateTime ReminderDateTime { get; set; } = DateTime.Now.AddDays(1);
        public RepeatingContext Repeating { get; set; } = new RepeatingContext(ERepeatTypes.NONE, 0, ERepeatDuration.FOREVER, 1, DateTime.Now.AddDays(7));
        public EReminderTypes ReminderTypes { get; set; } = EReminderTypes.NORMAL;
        public Guid CategoryId { get; set; } = Guid.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime NextRemind { get; set; } = DateTime.Now.AddDays(1);
        public bool IsForced { get; set; } = false;
        public bool IsImportant { get; set; } = false;
        public string Source { get; set; } = "";

        public ReminderContext(string name, DateTime reminderDateTime, RepeatingContext repeating, EReminderTypes reminderTypes, Guid categoryId, bool isForced, bool isImportant)
        {
            Id = Guid.NewGuid();
            Name = name;
            ReminderDateTime = reminderDateTime;
            Repeating = repeating;
            ReminderTypes = reminderTypes;
            CategoryId = categoryId;
            IsForced = isForced;
            IsImportant = isImportant;
        }
    }
}
