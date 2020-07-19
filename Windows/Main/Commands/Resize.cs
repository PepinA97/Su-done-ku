using System;
using System.Windows.Input;

namespace SudokuSolver.Windows.Main.Commands
{
    class Resize : ICommand
    {
        ViewModel VM;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Windows.Resize.ViewModel viewModel = new Windows.Resize.ViewModel(VM.NumSubgridColumns, VM.NumSubgridRows,
                                                                              VM.NumInsideSubgridColumns, VM.NumInsideSubgridRows);

            if (viewModel.ShowWindow(new Windows.Resize.View()))
            {
                VM.NumSubgridColumns = viewModel.NumSubgridColumns;
                VM.NumSubgridRows = viewModel.NumSubgridRows;

                VM.NumInsideSubgridColumns = viewModel.NumInsideSubgridColumns;
                VM.NumInsideSubgridRows = viewModel.NumInsideSubgridRows;

                VM.ObservableGrid = VM.CreateObservableGrid();
                VM.OnPropertyChanged("ObservableGrid");
            }
        }

        public Resize(ViewModel vm)
        {
            VM = vm;
        }
    }
}
