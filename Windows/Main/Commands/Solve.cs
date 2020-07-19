using System;
using System.Windows.Input;

namespace SudokuSolver.Windows.Main.Commands
{
    class Solve : ICommand
    {
        ViewModel VM;
        Models.Cell[,,,] SolvedBoard;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            Models.Cell[,,,] UnsolvedBoard = VM.ObservableGridToBoard(VM.ObservableGrid);

            Solver.ResultType result = Solver.SolveBoard(UnsolvedBoard);

            SolvedBoard = UnsolvedBoard;

            VM.ResultStatus = result;

            VM.OnPropertyChanged("ResultStatus");

            if (result == Solver.ResultType.Solvable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            for (int subgridX = 0; subgridX < VM.NumSubgridColumns; subgridX++)
            {
                for (int subgridY = 0; subgridY < VM.NumSubgridRows; subgridY++)
                {
                    for (int X = 0; X < VM.NumInsideSubgridColumns; X++)
                    {
                        for (int Y = 0; Y < VM.NumInsideSubgridRows; Y++)
                        {
                            VM.ObservableGrid[subgridX][subgridY][X][Y].Number = SolvedBoard[subgridX, subgridY, X, Y].CompletedNumber();
                        }
                    }
                }
            }
        }

        public Solve(ViewModel vm)
        {
            VM = vm;
        }
    }
}
