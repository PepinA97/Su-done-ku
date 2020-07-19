using System.Text.RegularExpressions;
using System.Windows;

namespace SudokuSolver.Windows.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class View : Window
    {
        public View()
        {
            InitializeComponent();
        }

        private void NumberBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9]");

            // Only allow numbers between 1 and 9
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
