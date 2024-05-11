namespace ActivityPulse.Models
{
    internal class IntervallContext
    {
        public DateTime VonDate { get; set; }
        public DateTime BisDate { get; set; }
        public string Name { get; set; }

        public IntervallContext(DateTime vonDate, DateTime bisDate, string name)
        {
            this.VonDate = vonDate;
            this.BisDate = bisDate;
            this.Name = name;
        }
    }
}
