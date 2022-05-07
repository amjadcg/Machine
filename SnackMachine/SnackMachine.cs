using MoneyFactory;
using MoneyFactory.Resources;
using Stacks;

namespace SnackMachine
{
    public interface ISnackMachine
    {
        void Run();
    }

    public class SnackMachine : ISnackMachine
    {
        private readonly IProductService _productService;
        private readonly List<string> _inputs = new();
        private readonly MoneyDealer _moneyDealer = new();

        public SnackMachine(IProductService productService)
        {
            _productService = productService;
            _inputs.AddRange(_productService.GetProductSymbols());
            _inputs.Add("I");
            _inputs.Add("C");
        }

        public void Run()
        {
            _productService.PrintProductNames();
            Console.Write("Please Select Item to Purchase\n");
            Console.WriteLine("Cash Balance: " + _moneyDealer.GetCashBalance());
            Console.WriteLine("Card Balance: " + _moneyDealer.GetCardBalance());
            Console.Write("For Inserting Money Type 'I'\n");
            Console.Write("For Cancelling Operation Type 'C'\n");

            HandleInput(Console.ReadLine());
            Console.WriteLine("--------------------------------------------------");
        }

        private void HandleInput(string? input)
        {
            if (input == null || !_inputs.Contains(input))
            {
                Console.Write("Bad Input, Please Try Again!\n");
                Run();
            }

            switch (input)
            {
                case "I":
                    _moneyDealer.Insert();
                    break;
                case "C":
                    var (card, coins, notes, isSuccess) = _moneyDealer.CancelOperation(_moneyDealer.GetCashBalance());
                    ReturnMoney(card, coins, notes);
                    break;
                default:
                    Purchase(input!);
                    break;
            }
            Run();
        }

        private void Purchase(string input)
        {
            if(!_productService.ProductExist(input)) { Console.WriteLine("Product Does not Exist");}
            var item = _productService.GetProduct(input);
            var (card, coins, notes, isSuccess) = _moneyDealer.Purchase(item.Price);
            if (isSuccess)
            {
                Console.WriteLine("Please Collect Your Snack\n");
                _productService.DispenseProduct(input);
                ReturnMoney(card, coins, notes);
            }
        }

        private void ReturnMoney(Card? card, List<Coin> coins, List<Note> notes)
        {
            if (card != null)
                Console.WriteLine("Please Collect your Card with Amount: " + card.Amount + "\n");

            Console.WriteLine("Please Collect your Coins");
            Console.WriteLine("Returned Coins:");
            foreach (var coin in coins)
            {
                Console.WriteLine($"Coin of {coin.Amount} Dollars");
            }

            Console.WriteLine("Please Collect your Notes");
            Console.WriteLine("Returned Notes:");
            foreach (var note in notes)
            {
                Console.WriteLine($"Note of {note.Amount} Dollars");
            }
        }
    }
}
