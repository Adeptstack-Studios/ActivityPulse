namespace ActivityPulse.Models
{
    internal class IntervallOfContext
    {
        public string Name { get; set; }
        public List<IntervallContext> Intervalls { get; set; } = new();
        public string IconPath { get; set; }

        public IntervallOfContext()
        {

        }

        public IntervallOfContext(string name, List<IntervallContext> intervalls, string iconPath)
        {
            this.Name = name;
            this.Intervalls = intervalls;
            IconPath = iconPath;
        }
    }
}
