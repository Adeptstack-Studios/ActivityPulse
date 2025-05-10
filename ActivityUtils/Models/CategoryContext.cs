using ActivityUtils.Data;
using ActivityUtils.Utils;

namespace ActivityUtils.Models
{
    public class CategoryContext
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public CategoryContext(string name)
        {
            Id = ReminderUtils.GetUniqueCategoryID(DataReminders.LoadCategories());
            Name = name;
        }

        public CategoryContext() { }
    }
}
