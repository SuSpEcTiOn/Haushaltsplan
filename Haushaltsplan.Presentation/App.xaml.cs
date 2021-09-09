namespace Haushaltsplan.Presentation
{
    using Haushaltsplan.Business.Manager;
    using Haushaltsplan.Business.Repository;
    using System.Windows;

    public partial class App : Application
    {
        public App()
        {
            var repository = new HaushaltsplanRepository();
            var manager = new HaushaltsplanManager(repository);
            var viewModel = new ViewModel(manager);
            var view = new MainWindow(viewModel);
            view.Show();
        }
    }
}
