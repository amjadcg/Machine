using MoneyFactory.Resources;
using MoneyFactory.Resources.Enums;
using static MoneyFactory.Resources.Enums.NoteCategories;

namespace MoneyFactory
{
    public class NoteBox : BaseMoneyBox, IMoneyBox
    {
        private static readonly NoteCategories[] AcceptedNotes = { TwentyUnits, FiftyUnits };
        private readonly List<Note> _notes = new();
        private double _balance;

        public double GetBalance() => _balance;


        public void ReturnNotes(List<Note> notes)
        {
            _notes.AddRange(notes);
            _balance += notes.Sum(x => x.Amount);
        }

        public Note? InsertNote(Note note)
        {
            if (!AcceptedCurrencies.Contains(note.Currency))
            {
                Console.WriteLine("Note Currency is not Supported, Please Try with Another!");
                return note;
            }

            if (!AcceptedNotes.Contains(note.Category))
            {
                Console.WriteLine("This Note is Not Accepted, Please Try with Another!");
                return note;
            }

            _notes.Add(note);
            _balance += note.Amount;
            Console.WriteLine($"Note with Amount: {note.Amount} {note.Currency} Inserted!");

            return null;
        }

        public (List<Note>, double) GetNotesChange(double amount)
        {
            //checking if there is notes change for requested amount
            var notesToReturn = new List<Note>();
            while (amount >= 20)
            {
                var note = _notes.Where(x => x.Amount <= amount)
                    .OrderByDescending(x => x.Amount).FirstOrDefault();
                if (note == null) break;
                amount -= note.Amount;
                notesToReturn.Add(note);
                _balance -= note.Amount;
                _notes.Remove(note);
            }

            return (notesToReturn, Math.Round(amount, 2));
        }
    }
}
