using SudokuSolver.Models;

namespace SudokuSolver
{
    static class CellHelper
    {
        public static void SetPossibility(Cell cell, int number)
        {
            for (int i = 1; i < 10; i++)
            {
                cell.Possibilities[i] = Constants.Possible;

                if (i != number)
                {
                    cell.Possibilities[i] = Constants.NotPossible;
                }
            }
        }

        public static void RuleOutPossibility(Cell cell, int number)
        {
            cell.Possibilities[number] = Constants.NotPossible;
        }
    }
}
