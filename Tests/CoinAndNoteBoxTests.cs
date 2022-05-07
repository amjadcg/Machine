using MoneyFactory;
using MoneyFactory.Resources;
using MoneyFactory.Resources.Enums;
using System.Linq;
using Xunit;

namespace Tests
{
    public class CoinAndNoteBoxTests
    {
        private CoinBox _coinBox;
        private NoteBox _noteBox;

        public CoinAndNoteBoxTests()
        {
            _noteBox = new NoteBox();
            _coinBox = new CoinBox();
        }

        [Fact]
        public void InsertValidNotesThenAskForNotValidChangeChange()
        {
            _noteBox = new NoteBox();
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.TwentyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.TwentyUnits, Currencies.USD));


            //Sum is 240
            //Box should return empty list and remain of 3.1 because it does not has the amount
            var (notes, remain) = _noteBox.GetNotesChange(3.1);


            //We should have empty list and the same remaining amount
            Assert.Empty(notes);
            Assert.True(remain == 3.1);
            Assert.True(_noteBox.GetBalance() == 240); //box should still have its money
        }

        [Fact]
        public void InsertValidNotesThenAskForValidChangeChange()
        {
            //Insert Notes  {50, 50 , 50, 50, 20, 20} = 240
            _noteBox = new NoteBox();
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.TwentyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.TwentyUnits, Currencies.USD));

            //Insert Coins {1, 1, 1, 0.1, 0.1, 0.2, 0.2, 0.5} = 4.1
            _coinBox = new CoinBox();
            _coinBox.InsertCoin(new Coin(CoinCategories.OneUnit, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.OneUnit, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.OneUnit, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.TenUnitCents, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.TenUnitCents, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.TwentyUnitCents, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.TwentyUnitCents, Currencies.USD));
            _coinBox.InsertCoin(new Coin(CoinCategories.FiftyUnitCents, Currencies.USD));


            //Assume we need to take change of 152.2
            //The service will call the two boxes in order to take the change
            //Box Sum is 240
            //Box should return list of notes that have sum of requested change
            var (notes, remain) = _noteBox.GetNotesChange(150.2);

            //We should have a valid list with the same remaining amount
            Assert.NotNull(notes);
            Assert.True(remain == 0.2);
            Assert.True(_noteBox.GetBalance() == 240 - 150); //box should have its money . 
            Assert.True(notes.Sum(x => x.Amount) == 150); 


            //check the coin box
            //Box Sum is 4.1
            //Box should return 0.2 coin
            var (coins, remain2) = _coinBox.GetCoinsChange(remain);

            //We should have a valid list with the same remaining amount
            Assert.NotNull(coins);
            Assert.True(remain2 == 0);
            Assert.True(_coinBox.GetBalance() == 3.9); //box should have its money . 
            Assert.True(coins.Sum(x => x.Amount) == 0.2); 
        }
    }
}