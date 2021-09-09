namespace Haushaltsplan.Presentation
{
    using System.Windows;

    public partial class MainWindow : Window
    {
        public MainWindow(ViewModel viewModel)
        {
            DataContext = ViewModel = viewModel;
            InitializeComponent();
        }

        private ViewModel ViewModel { get; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadData();
        }
    }
}
