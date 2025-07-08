using ActivityUtils.Enums;

namespace ActivityHost
{
    class Utils
    {
        public static string GetSound(EReminderTypes type, int dayRepeat)
        {
            if (dayRepeat == 0)
            {
                switch (type)
                {
                    case EReminderTypes.NORMAL:
                        return "sounds/1-1.wav";
                    case EReminderTypes.BREAK:
                        return "sounds/2-1.wav";
                    case EReminderTypes.POWEROFF:
                        return "sounds/3-1.wav";
                }
            }
            else if (dayRepeat == 1)
            {
                switch (type)
                {
                    case EReminderTypes.NORMAL:
                        return "sounds/1-2.wav";
                    case EReminderTypes.BREAK:
                        return "sounds/2-2.wav";
                    case EReminderTypes.POWEROFF:
                        return "sounds/3-2.wav";
                }
            }
            else if (dayRepeat >= 2)
            {
                switch (type)
                {
                    case EReminderTypes.NORMAL:
                        return "sounds/1-3.wav";
                    case EReminderTypes.BREAK:
                        return "sounds/2-3.wav";
                    case EReminderTypes.POWEROFF:
                        return "sounds/3-3.wav";
                }
            }
            return "sounds/3-3.wav";
        }
    }
}
