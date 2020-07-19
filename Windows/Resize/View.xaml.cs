using System.Text.RegularExpressions;
using System.Windows;

namespace SudokuSolver.Windows.Resize
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {
        public View()
        {
            InitializeComponent();
        }

        private void NumSubgridColumnsTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsValidInput(e);
        }

        private void NumSubgridRowsTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsValidInput(e);
        }

        private void NumInsideSubgridColumnsTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsValidInput(e);
        }

        private void NumInsideSubgridRowsTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsValidInput(e);
        }

        bool IsValidInput(System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^2-9]");

            return regex.IsMatch(e.Text);
        }
    }
}
