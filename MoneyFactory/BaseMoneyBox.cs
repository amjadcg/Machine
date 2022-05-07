using MoneyFactory.Resources.Enums;
using static MoneyFactory.Resources.Enums.Currencies;

namespace MoneyFactory
{
    public abstract class BaseMoneyBox
    {
        protected static readonly Currencies[] AcceptedCurrencies = { USD };
    }
}
