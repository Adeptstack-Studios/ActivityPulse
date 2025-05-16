using ActivityUtils.Enums;

namespace ActivityUtils.Models
{
    public class RepeatingContext
    {
        public ERepeatTypes RepeatType { get; set; } = ERepeatTypes.DAILY;
        public int RepeatInterval { get; set; } = 0;
        public ERepeatDuration RepeatDuration { get; set; } = ERepeatDuration.FOREVER;
        public int RepeatCount { get; set; } = 1;
        public DateTime RepeatUntil { get; set; } = DateTime.Now.AddDays(7);

        public RepeatingContext(ERepeatTypes repeatType, int repeatInterval, ERepeatDuration repeatDuration, int repeatCount, DateTime repeatUntil)
        {
            RepeatType = repeatType;
            RepeatInterval = repeatInterval;
            RepeatDuration = repeatDuration;
            RepeatCount = repeatCount;
            RepeatUntil = repeatUntil;
        }

        public RepeatingContext() { }
    }
}
