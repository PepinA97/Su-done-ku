using System;
using System.Windows.Input;

namespace SudokuSolver.Windows.Main.Commands
{
    class Clear : ICommand
    {
        ViewModel VM;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            for (int subgridX = 0; subgridX < VM.NumSubgridColumns; subgridX++)
            {
                for (int subgridY = 0; subgridY < VM.NumSubgridRows; subgridY++)
                {
                    for (int X = 0; X < VM.NumInsideSubgridColumns; X++)
                    {
                        for (int Y = 0; Y < VM.NumInsideSubgridRows; Y++)
                        {
                            if (VM.ObservableGrid[subgridX][subgridY][X][Y].Number.HasValue)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
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
                            VM.ObservableGrid[subgridX][subgridY][X][Y].Number = null;
                        }
                    }
                }
            }
        }

        public Clear(ViewModel vm)
        {
            VM = vm;
        }
    }
}
