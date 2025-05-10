using KinderApp.Commands;
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

            // Создаем экземпляр RegistrationWindowModel (или получаем его из IoC контейнера)
            ViewModelBase viewModel = new (); 

            // Создаем экземпляр RegistrationWindow, передавая ViewModel
            RegistrationWindow registrationWindow = new RegistrationWindow(viewModel);

            // Показываем окно
            registrationWindow.Show();
        }
    }

}
