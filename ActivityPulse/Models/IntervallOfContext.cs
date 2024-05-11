namespace ActivityPulse.Models
{
    internal class IntervallOfContext
    {
        public string Name { get; set; }
        public List<IntervallContext> Intervalls { get; set; } = new();

        public IntervallOfContext()
        {

        }

        public IntervallOfContext(string name, List<IntervallContext> intervalls)
        {
            this.Name = name;
            this.Intervalls = intervalls;
        }
    }
}
