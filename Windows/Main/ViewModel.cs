using SudokuSolver.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace SudokuSolver.Windows.Main
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region On Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<ObservableCollection<ObservableCollection<ObservableCollection<ObservableCell>>>> ObservableGrid { get; set; }
        #endregion

        public ICommand Solve { get; set; }
        public ICommand Clear { get; set; }
        public ICommand Resize { get; set; }

        public int NumSubgridColumns { get; set; }
        public int NumSubgridRows { get; set; }
        public int NumInsideSubgridColumns { get; set; }
        public int NumInsideSubgridRows { get; set; }

        public Solver.ResultType ResultStatus { get; set; }

        public Cell[,,,] ObservableGridToBoard(ObservableCollection<ObservableCollection<ObservableCollection<ObservableCollection<ObservableCell>>>> observableGrid)
        {
            Cell[,,,] board = BoardFactory.CreateBoard(observableGrid.Count, observableGrid[0].Count, observableGrid[0][0].Count, observableGrid[0][0][0].Count);

            for (int subgridX = 0; subgridX < board.GetLength(0); subgridX++)
            {
                for (int subgridY = 0; subgridY < board.GetLength(1); subgridY++)
                {
                    for (int X = 0; X < board.GetLength(2); X++)
                    {
                        for (int Y = 0; Y < board.GetLength(3); Y++)
                        {
                            if (observableGrid[subgridX][subgridY][X][Y].Number.HasValue)
                            {
                                CellHelper.SetPossibility(board[subgridX, subgridY, X, Y], observableGrid[subgridX][subgridY][X][Y].Number.GetValueOrDefault());
                            }
                        }
                    }
                }
            }

            return board;
        }

        public ObservableCollection<ObservableCollection<ObservableCollection<ObservableCollection<ObservableCell>>>> CreateObservableGrid()
        {
            ObservableGrid = new ObservableCollection<ObservableCollection<ObservableCollection<ObservableCollection<ObservableCell>>>>();

            for (int subgridX = 0; subgridX < NumSubgridColumns; subgridX++)
            {
                ObservableGrid.Add(new ObservableCollection<ObservableCollection<ObservableCollection<ObservableCell>>>());

                for (int subgridY = 0; subgridY < NumSubgridRows; subgridY++)
                {
                    ObservableGrid[subgridX].Add(new ObservableCollection<ObservableCollection<ObservableCell>>());

                    for (int X = 0; X < NumInsideSubgridColumns; X++)
                    {
                        ObservableGrid[subgridX][subgridY].Add(new ObservableCollection<ObservableCell>());

                        for (int Y = 0; Y < NumInsideSubgridRows; Y++)
                        {
                            ObservableGrid[subgridX][subgridY][X].Add(new ObservableCell(null));
                        }
                    }
                }
            }

            return ObservableGrid;
        }

        public void ShowWindow(View view)
        {
            view.DataContext = this;
            view.ShowDialog();
        }

        public ViewModel()
        {
            Solve = new Commands.Solve(this);
            Clear = new Commands.Clear(this);
            Resize = new Commands.Resize(this);

            NumSubgridColumns = Constants.DefaultSize.NumSubgridColumns;
            NumSubgridRows = Constants.DefaultSize.NumSubgridRows;
            NumInsideSubgridColumns = Constants.DefaultSize.NumInsideSubgridColumns;
            NumInsideSubgridRows = Constants.DefaultSize.NumInsideSubgridRows;

            ObservableGrid = CreateObservableGrid();
        }
    }

    public class ObservableCell : INotifyPropertyChanged
    {
        #region On Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        int? _Number;
        public int? Number
        {
            get
            {
                return _Number;
            }
            set
            {
                _Number = value;
                OnPropertyChanged("Number");

                // Notify commands
                OnPropertyChanged("Solve");
                OnPropertyChanged("Clear");
            }
        }

        public ObservableCell(int? number)
        {
            Number = number;
        }
    }
}
