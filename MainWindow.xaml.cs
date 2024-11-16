using System.Windows;

namespace CRMApp
{
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            var clientWindow = new ClientWindow();
            clientWindow.ShowDialog();
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            var orderWindow = new OrderWindow();
            orderWindow.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void TasksButton_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new TaskWindow();
            taskWindow.ShowDialog();
        }
    }
}
