using MoneyFactory.Resources.BaseClasses;
using MoneyFactory.Resources.Enums;

namespace MoneyFactory.Resources
{
    public class Coin : MoneyContainer
    {
        public Coin(CoinCategories category, Currencies currency)
        {
            Category = category;
            Currency = currency;
            Amount = category switch
            {
                CoinCategories.TenUnitCents => 0.1,
                CoinCategories.TwentyUnitCents => 0.2,
                CoinCategories.FiftyUnitCents => 0.5,
                CoinCategories.OneUnit => 1.0,
                CoinCategories.TwoUnits => 2.0,
                _ => 0.00
            };
        }
        public CoinCategories Category { get; set; }

        public double Diameter { get; set; }

        public double Weight { get; set; }
    }
}