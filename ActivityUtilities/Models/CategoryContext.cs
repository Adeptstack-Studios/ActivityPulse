namespace ActivityUtilities.Models
{
    public class CategoryContext
    {
        public Guid Id { get; }
        public string Name { get; set; } = "";

        public CategoryContext(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
