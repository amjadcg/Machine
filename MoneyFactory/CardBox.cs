using MoneyFactory.Resources;
using MoneyFactory.Resources.Enums;
using static MoneyFactory.Resources.Enums.CardCategories;

namespace MoneyFactory
{
    public class CardBox : BaseMoneyBox, IMoneyBox
    {
        private static readonly CardCategories[] AcceptedCards = { Visa, MasterCard };
        private Card? _card;

        public double GetBalance() => _card?.Amount ?? 0.0;

        public Card? GetCard() => _card;


        public Card? InsertCard(Card card)
        {
            if (_card != null)
            {
                Console.WriteLine("There is Already a Card in the Box!, Hit Cancel Then Try again!");
                return card;
            }

            if (!AcceptedCurrencies.Contains(card.Currency))
            {
                Console.WriteLine("Card Currency is not Supported, Please Try with Another!");
                return card;
            }

            if (!card.IsValid)
            {
                Console.WriteLine("This Card is Not Accepted, Please Try with Another!");
                return card;
            }

            _card = card;
            Console.WriteLine($"Valid Card with Amount: {card.Amount} Inserted!");
            return null;
        }

        public Card SubtractAndGetCard(double amount)
        {
            var card = _card;
            card!.Amount -= amount;
            _card = null;
            return card;
        }
    }
}

