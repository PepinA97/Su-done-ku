using SudokuSolver.Models;
using System.Collections;
using System.ComponentModel;

namespace SudokuSolver
{
    public class Solver
    {
        public enum ResultType
        {
            Unsolvable,
            NotEnoughNumbers,
            Solvable
        }

        public static ResultType SolveBoard(Cell[,,,] board)
        {
            Stack stack = new Stack();

            SeedStack(board, stack);

            while (true)
            {
                if (stack.Count == 0)
                {
                    break;
                }

                FillIn currentCell = (FillIn)stack.Pop();

                MarkSubgrid(currentCell, board, stack);
                MarkRow(currentCell, board, stack);
                MarkColumn(currentCell, board, stack);
            }

            return DetermineResult(board);
        }

        static void SeedStack(Cell[,,,] board, Stack stack)
        {
            for (int subgridX = 0; subgridX < board.GetLength(Constants.Dimensions.SubgridX); subgridX++)
            {
                for (int subgridY = 0; subgridY < board.GetLength(Constants.Dimensions.SubgridY); subgridY++)
                {
                    for (int X = 0; X < board.GetLength(Constants.Dimensions.InsideSubgridX); X++)
                    {
                        for (int Y = 0; Y < board.GetLength(Constants.Dimensions.InsideSubgridY); Y++)
                        {
                            if (board[subgridX, subgridY, X, Y].IsCompleted())
                            {
                                int? num = board[subgridX, subgridY, X, Y].CompletedNumber();

                                stack.Push(new FillIn(subgridX, subgridY, X, Y, num));

                                board[subgridX, subgridY, X, Y].Checked = true;
                            }
                        }
                    }
                }
            }
        }

        static void MarkSubgrid(FillIn fillIn, Cell[,,,] board, Stack completedCells)
        {
            for (int X = 0; X < board.GetLength(Constants.Dimensions.InsideSubgridX); X++)
            {
                for (int Y = 0; Y < board.GetLength(Constants.Dimensions.InsideSubgridY); Y++)
                {
                    if ((X != fillIn.InsideSubgrid.X) || (Y != fillIn.InsideSubgrid.Y))
                    {
                        Mark(board, completedCells, fillIn.Number.GetValueOrDefault(), fillIn.Subgrid.X, fillIn.Subgrid.Y, X, Y);
                    }
                }
            }
        }

        static void MarkRow(FillIn fillIn, Cell[,,,] board, Stack completedCells)
        {
            for (int subgridX = 0; subgridX < board.GetLength(Constants.Dimensions.SubgridX); subgridX++)
            {
                for (int X = 0; X < board.GetLength(Constants.Dimensions.InsideSubgridX); X++)
                {
                    if (subgridX != fillIn.Subgrid.X)
                    {
                        Mark(board, completedCells, fillIn.Number.GetValueOrDefault(), subgridX, fillIn.Subgrid.Y, X, fillIn.InsideSubgrid.Y);
                    }
                }
            }
        }

        static void MarkColumn(FillIn fillIn, Cell[,,,] board, Stack completedCells)
        {
            for (int subgridY = 0; subgridY < board.GetLength(Constants.Dimensions.SubgridY); subgridY++)
            {
                for (int Y = 0; Y < board.GetLength(Constants.Dimensions.InsideSubgridY); Y++)
                {
                    if (subgridY != fillIn.Subgrid.Y)
                    {
                        Mark(board, completedCells, fillIn.Number.GetValueOrDefault(), fillIn.Subgrid.X, subgridY, fillIn.InsideSubgrid.X, Y);
                    }
                }
            }
        }

        static void Mark(Cell[,,,] board, Stack completedCells, int ruleout, int subgridX, int subgridY, int X, int Y)
        {
            CellHelper.RuleOutPossibility(board[subgridX, subgridY, X, Y], ruleout);

            if (board[subgridX, subgridY, X, Y].IsCompleted())
            {
                int? num = board[subgridX, subgridY, X, Y].CompletedNumber();

                if (!board[subgridX, subgridY, X, Y].Checked)
                {
                    completedCells.Push(new FillIn(subgridX, subgridY, X, Y, num));

                    board[subgridX, subgridY, X, Y].Checked = true;
                }
            }
        }

        static ResultType DetermineResult(Cell[,,,] board)
        {
            ResultType result = ResultType.Solvable;

            for (int subgridX = 0; subgridX < board.GetLength(Constants.Dimensions.SubgridX); subgridX++)
            {
                for (int subgridY = 0; subgridY < board.GetLength(Constants.Dimensions.SubgridY); subgridY++)
                {
                    for (int X = 0; X < board.GetLength(Constants.Dimensions.InsideSubgridX); X++)
                    {
                        for (int Y = 0; Y < board.GetLength(Constants.Dimensions.InsideSubgridY); Y++)
                        {
                            if (!board[subgridX, subgridY, X, Y].IsCompleted())
                            {
                                result = ResultType.NotEnoughNumbers;
                            }

                            if (board[subgridX, subgridY, X, Y].GetPossibilitiesCount() == 0)
                            {
                                return ResultType.Unsolvable;
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}