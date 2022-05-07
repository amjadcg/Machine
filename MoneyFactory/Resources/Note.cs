using MoneyFactory.Resources.BaseClasses;
using MoneyFactory.Resources.Enums;

namespace MoneyFactory.Resources
{
    public class Note : MoneyContainer
    {
        public Note(NoteCategories category, Currencies currency)
        {
            Category = category;
            Currency = currency;
            Amount = category switch
            {
                NoteCategories.TwentyUnits => 20.0,
                NoteCategories.FiftyUnits => 50.0,
                NoteCategories.HundredUnits => 100.0,
                _ => 0.0
            };
        }

        public NoteCategories Category { get; set; }

        public double Width { get; set; }

        public double Length { get; set; }

        public string? SerialNumber { get; set; }
    }
}