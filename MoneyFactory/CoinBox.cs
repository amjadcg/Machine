using MoneyFactory.Resources;
using MoneyFactory.Resources.Enums;
using static MoneyFactory.Resources.Enums.CoinCategories;

namespace MoneyFactory
{
    public class CoinBox : BaseMoneyBox, IMoneyBox
    {
        private static readonly CoinCategories[] AcceptedCoins = { TenUnitCents, TwentyUnitCents, FiftyUnitCents, OneUnit };
        private readonly List<Coin> _coins = new();
        private double _balance;

        public double GetBalance() => _balance;

        public Coin? InsertCoin(Coin coin)
        {
            if (!AcceptedCurrencies.Contains(coin.Currency))
            {
                Console.WriteLine("Coin Currency is not Supported, Please Try with Another!");
                return coin;
            }

            if (!AcceptedCoins.Contains(coin.Category))
            {
                Console.WriteLine("This Coin is Not Accepted, Please Try with Another!");
                return coin;
            }

            _coins.Add(coin);
            _balance += coin.Amount;
            Console.WriteLine($"Coin with Amount: {coin.Amount} {coin.Currency} Inserted!");

            return null;
        }

        public (List<Coin>, double) GetCoinsChange(double amount)
        {
            //Here we do the change problem after amount was checked in notes box
            var coinsToReturn = new List<Coin>(); //coins to return in list
            var amountToReturn = amount;
            var balance = _balance;
            while (0.1 <= Math.Round(amountToReturn, 2) && Math.Round(amountToReturn, 2) <= Math.Round(balance, 2))
            {
                var coin = _coins.Where(x => x.Amount <= Math.Round(amountToReturn, 2))
                    .OrderByDescending(x => x.Amount).FirstOrDefault();
                if (coin == null) break;
                amountToReturn -= coin.Amount;
                amountToReturn = Math.Round(amountToReturn, 2);
                coinsToReturn.Add(coin); //if change is available
                balance -= Math.Round(coin.Amount, 2);
                _coins.Remove(coin);
            }

            if (amountToReturn == 0)
            {
                _balance = Math.Round(balance, 2); //change was found, returning list of coins with remain = 0
                return (coinsToReturn, amountToReturn);
            }
            
            _coins.AddRange(coinsToReturn); //roll back if remain != Zero
            return (new List<Coin>(), amount); //return empty list of coins
        }
    }
}
