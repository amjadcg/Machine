using MoneyFactory;
using MoneyFactory.Resources;
using MoneyFactory.Resources.Enums;
using System.Linq;
using Xunit;

namespace Tests
{
    public class NoteBoxTests
    {
        private NoteBox _noteBox;

        public NoteBoxTests()
        {
            _noteBox = new NoteBox();
        }

        [Fact]
        public void InsertUnSupportedNote()
        {
            var result = _noteBox.InsertNote(new Note(NoteCategories.HundredUnits, Currencies.USD));

            //If we try to insert unsupported note, the box will return the note back
            Assert.NotNull(result);
            Assert.True(result!.Amount == 100);
        }

        [Fact]
        public void InsertUnSupportedCurrency()
        {
            var result = _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.EUR));

            //If we try to insert unsupported currency, the box will return the note back
            Assert.NotNull(result);
            Assert.True(result!.Amount == 50);
        }

        [Fact]
        public void InsertValidNote_BalanceShouldBeChanged()
        {
            var result = _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));

            //If we inserted a valid note, the box will take it and return nothing.
            Assert.Null(result);

            Assert.True(_noteBox.GetBalance() == 50);
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
            _noteBox = new NoteBox();
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.FiftyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.TwentyUnits, Currencies.USD));
            _noteBox.InsertNote(new Note(NoteCategories.TwentyUnits, Currencies.USD));


            //Sum is 240
            //Box should return list of notes that have sum of requested change
            var (notes, remain) = _noteBox.GetNotesChange(170);


            //We should have a valid list with the same remaining amount
            Assert.NotNull(notes);
            Assert.True(remain == 0);
            Assert.True(_noteBox.GetBalance() == 240 - 170); //box should have its money . 
            Assert.True(notes.Sum(x => x.Amount) == 170); //box should have its money .
        }
    }
}