using MoneyFactory.Resources;
using MoneyFactory.Resources.Enums;

namespace MoneyFactory
{
    public interface IMoneyDealer
    {
        double GetCashBalance();
        double GetCardBalance();
        (Card?, List<Coin>, List<Note>, bool) Purchase(double amount);
        (Card?, List<Coin>, List<Note>, bool) CancelOperation(double insertedAmount);
        void Insert();
    }

    public class MoneyDealer : IMoneyDealer
    {
        private readonly CardBox _cardBox = new();
        private readonly NoteBox _noteBox = new();
        private readonly CoinBox _coinBox = new();
        private double _cashBalance;

        public double GetCashBalance() => Math.Round(_cashBalance, 2);
        public double GetCardBalance() => _cardBox.GetBalance();

        public (Card?, List<Coin>, List<Note>, bool) Purchase(double amount)
        {
            if (!(GetCardBalance() >= amount)) return PurchaseFromCash(amount);
            Console.WriteLine("Price is: " + amount);
            Console.WriteLine("Purchasing from Card with Price: " + amount);
            Console.WriteLine("Purchase Completed Successfully!");
            return (_cardBox.SubtractAndGetCard(amount), new List<Coin>(), new List<Note>(), true);
        }

        public (Card?, List<Coin>, List<Note>, bool) CancelOperation(double insertedAmount)
        {
            //Cancel and return collected money
            var (notes, remain) = _noteBox.GetNotesChange(insertedAmount);
            var (coins, remain2) = _coinBox.GetCoinsChange(remain);

            if (remain2 == 0)
            {
                _cashBalance = 0.0;
                return (_cardBox.GetCard(), coins, notes, false);
            }

            Console.WriteLine("Error");
            return (_cardBox.GetCard(), new List<Coin>(), new List<Note>(), false);
        }

        public void Insert()
        {
            Console.Write("1 - Coin\n");
            Console.Write("2 - Note\n");
            Console.Write("3 - Card\n");

            var chosen = Console.ReadLine();

            switch (chosen)
            {
                case "1":
                    InsertCoin();
                    break;
                case "2":
                    InsertNote();
                    break;
                case "3":
                    InsertCard();
                    break;
                default:
                    Console.Write("Bad Input, Please Try Again!\n");
                    break;
            }
        }

        private (Card?, List<Coin>, List<Note>, bool) PurchaseFromCash(double amount)
        {
            Console.WriteLine("Item Price is: " + amount);

            if (amount > _cashBalance)
            {
                Console.WriteLine("Insufficient Fund, Please Insert More Money or a Valid Card");
                return (null, new List<Coin>(), new List<Note>(), false);
            }

            if (amount < _cashBalance) return SubtractAndReturnChange(amount);
            Console.WriteLine("Price is: " + amount);
            Console.WriteLine("Purchasing from Cash with Price: " + amount);
            Console.WriteLine("Purchase Completed Successfully!");
            Console.WriteLine("No Change to Return");
            _cashBalance = 0.0;
            return (_cardBox.GetCard(), new List<Coin>(), new List<Note>(), true);
        }

        private (Card?, List<Coin>, List<Note>, bool) SubtractAndReturnChange(double amount)
        {
            Console.WriteLine("Checking if there is change");

            var (notes, remain) = _noteBox.GetNotesChange(_cashBalance - amount);
            var (coins, remain2) = _coinBox.GetCoinsChange(remain);

            if (remain == 0)
            {
                _cashBalance = 0.0;
                return (_cardBox.GetCard(), new List<Coin>(), notes, true);
            }

            if (remain2 == 0)
            {
                _cashBalance = 0.0;
                return (_cardBox.GetCard(), coins, notes, true);
            }
            _noteBox.ReturnNotes(notes); //return notes to the box
            Console.WriteLine("There is no change please take your money back");
            return CancelOperation(_cashBalance);
        }

        private void InsertNote()
        {
            Console.Write("1 - 20 Dollar Note\n");
            Console.Write("2 - 50 Dollar Note\n");

            var chosen = Console.ReadLine();
            switch (chosen)
            {
                case "1":
                    if (_noteBox.InsertNote(new Note(NoteCategories.TwentyUnits, Currencies.USD)) == null)
                        _cashBalance += 20;
                    break;
                case "2":
                    if (_noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD)) == null)
                        _cashBalance += 50;
                    break;
                default:
                    Console.Write("Bad Input, Please Try Again!\n");
                    break;
            }
        }

        private void InsertCoin()
        {
            Console.Write("1 - 10 Cents Dollar Coin\n");
            Console.Write("2 - 20 Cents Dollar Coin\n");
            Console.Write("3 - 50 Cents Dollar Coin\n");
            Console.Write("4 - 1 Dollar Coin\n");
            var chosen = Console.ReadLine();
            switch (chosen)
            {
                case "1":
                    if (_coinBox.InsertCoin(new Coin(CoinCategories.TenUnitCents, Currencies.USD)) == null)
                        _cashBalance += 0.1;
                    break;
                case "2":
                    if (_coinBox.InsertCoin(new Coin(CoinCategories.TwentyUnitCents, Currencies.USD)) == null)
                        _cashBalance += 0.2;
                    break;
                case "3":
                    if (_coinBox.InsertCoin(new Coin(CoinCategories.FiftyUnitCents, Currencies.USD)) == null)
                        _cashBalance += 0.5;
                    break;
                case "4":
                    if (_coinBox.InsertCoin(new Coin(CoinCategories.OneUnit, Currencies.USD)) == null)
                        _cashBalance += 1;
                    break;
                default:
                    Console.Write("Bad Input, Please Try Again!\n");
                    break;
            }
        }

        private void InsertCard()
        {
            Console.Write("Inserting Card of amount 5 Dollars\n");
            _cardBox.InsertCard(new Card(CardCategories.Visa, 5, true, Currencies.USD));
        }
    }
}
