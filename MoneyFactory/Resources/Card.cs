using MoneyFactory.Resources.BaseClasses;
using MoneyFactory.Resources.Enums;

namespace MoneyFactory.Resources
{
    public class Card : MoneyContainer
    {
        public Card(CardCategories category, double amount, bool isValid, Currencies currencies)
        {
            Category = category;
            Currency = currencies;
            Amount = amount;
            IsValid = isValid;
        }
        public CardCategories Category { get; set; }

        public string? HolderName { get; set; }

        public string? Cvv { get; set; }

        public string? SerialNumber { get; set; }

        public DateOnly EndDate { get; set; }
        public bool IsValid { get; set; }
    }
}