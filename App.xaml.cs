using System.Windows;

namespace SudokuSolver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            Windows.Main.ViewModel viewModel = new Windows.Main.ViewModel();

            viewModel.ShowWindow(new Windows.Main.View());

            Shutdown();
        }
    }
}
