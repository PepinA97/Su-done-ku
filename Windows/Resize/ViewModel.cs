using GalaSoft.MvvmLight.Command;

namespace SudokuSolver.Windows.Resize
{
    class ViewModel
    {
        public int NumSubgridColumns { get; set; }
        public int NumSubgridRows { get; set; }

        public int NumInsideSubgridColumns { get; set; }
        public int NumInsideSubgridRows { get; set; }

        public RelayCommand Finish { get; set; }
        public RelayCommand Cancel { get; set; }

        View View;

        public void FinishExecute()
        {
            View.DialogResult = true;

            View.Close();
        }
        
        public bool FinishCanExecute()
        {
            return true;
        }

        public void CancelExecute()
        {
            View.DialogResult = false;

            View.Close();
        }

        public bool CancelCanExecute()
        {
            return true;
        }

        public bool ShowWindow(View view)
        {
            View = view;
            view.DataContext = this;

            return view.ShowDialog().GetValueOrDefault();
        }

        public ViewModel(int numSubgridColumns, int numSubgridRows,
                         int numInsideSubgridColumns, int numInsideSubgridRows)
        {
            Finish = new RelayCommand(FinishExecute, FinishCanExecute);
            Cancel = new RelayCommand(CancelExecute, CancelCanExecute);

            NumSubgridColumns = numSubgridColumns;
            NumSubgridRows = numSubgridRows;

            NumInsideSubgridColumns = numInsideSubgridColumns;
            NumInsideSubgridRows = numInsideSubgridRows;
        }
    }
}
