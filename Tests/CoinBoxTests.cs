using System.Linq;
using MoneyFactory;
using MoneyFactory.Resources;
using MoneyFactory.Resources.Enums;
using Xunit;

namespace Tests
{
    public class CoinBoxTests
    {
        private CoinBox _coinBox;

        public CoinBoxTests()
        {
            _coinBox = new CoinBox();
        }

        [Fact]
        public void InsertUnSupportedCoin()
        {
            var result = _coinBox.InsertCoin(new Coin(CoinCategories.TwoUnits, Currencies.USD));

            //If we try to insert unsupported coin, the box will return the coin
            Assert.NotNull(result);
            Assert.True(result!.Amount == 2);
        }

        [Fact]
        public void InsertUnSupportedCurrency()
        {
            var result = _coinBox.InsertCoin(new Coin(CoinCategories.OneUnit, Currencies.EUR));

            //If we try to insert unsupported currency, the box will return the coin
            Assert.NotNull(result);
            Assert.True(result!.Amount == 1);
        }

        [Fact]
        public void InsertValidCoin_BalanceShouldbeChanged()
        {
            var result = _coinBox.InsertCoin(new Coin(CoinCategories.OneUnit, Currencies.USD));

            //If we inserted a valid coin, the box will take it and return nothing.
            Assert.Null(result);

            Assert.True(_coinBox.GetBalance() == 1);
        }

        [Fact]
        public void InsertValidCoinsThenAskForChange()
        {
            _coinBox = new CoinBox();
            _coinBox.InsertCoin(new Coin(CoinCategories.OneUnit, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.OneUnit, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.OneUnit, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.TenUnitCents, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.TwentyUnitCents, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.FiftyUnitCents, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.TenUnitCents, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.TwentyUnitCents, Currencies.USD));

            //Sum is 4.1
            //Box should return 3.1 because it has the amount
            var (coins, remain) = _coinBox.GetCoinsChange(3.1);


            //We should have three coins of 1 dollar and one coin of 10 cents.
            Assert.NotEmpty(coins);
            Assert.True(remain == 0);
            Assert.True(coins.Count == 4);
            Assert.True(coins.Sum(x => x.Amount) == 3.1);  //amount of change
            Assert.True(_coinBox.GetBalance() == 1);
        }
    }
}