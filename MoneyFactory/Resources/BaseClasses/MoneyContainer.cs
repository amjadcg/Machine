using MoneyFactory.Resources.Enums;

namespace MoneyFactory.Resources.BaseClasses
{
    public abstract class MoneyContainer
    {
        public Currencies Currency { get; set; }
        public double Amount;
    }
}
