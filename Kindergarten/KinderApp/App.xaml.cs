using KinderApp.Commands;
using KinderApp.ViewModels;
using KinderDbContext.Connections;
using System.Configuration;
using System.Data;
using System.Windows;

namespace KinderApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ViewModelBase viewModel = new RegistrationWindowModel(); 

            RegistrationWindow registrationWindow = new RegistrationWindow();

            // Показываем окно
            registrationWindow.Show();
        }
    }

}
