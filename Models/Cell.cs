using System.Linq;

namespace SudokuSolver.Models
{
    public class Cell
    {
        public bool[] Possibilities { get; set; }
        public bool Checked { get; set; }

        public Cell()
        {
            Possibilities = new bool[10];
            Possibilities[0] = Constants.NotPossible; // 0 isn't used, mark out as NOT a possibility

            Checked = false;
        }

        public int GetPossibilitiesCount()
        {
            // Counts number of false occurrences
            return Possibilities.Count(c => !c);
        }

        public bool IsCompleted()
        {
            int possibilitiesCount = GetPossibilitiesCount();

            if (possibilitiesCount > 1)
            {
                return false;
            }

            return true;
        }

        public int? CompletedNumber()
        {
            if (IsCompleted())
            {
                for (int number = 1; number < 10; number++)
                {
                    if (Possibilities[number] == Constants.Possible)
                    {
                        return number;
                    }
                }
            }

            return null;
        }
    }
}