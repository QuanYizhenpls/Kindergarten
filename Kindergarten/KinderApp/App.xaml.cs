using KinderApp.Commands;
using KinderApp.ViewModels;
using KinderDbContext.Connections;
using System.Configuration;
using System.Data;
using System.Windows;

namespace KinderApp
{
    /// <summary>
    /// Класс приложения, управляющий жизненным циклом и запуском WPF-приложения.
    /// Наследует встроенный класс Application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Переопределяет метод запуска приложения, вызываемый при инициализации.
        /// Выполняет настройку и запуск начальных компонентов приложения.
        /// </summary>
        /// <param name="e">Аргументы запуска, переданные при старте приложения.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Вызов базовой реализации метода для корректной работы стандартных механизмов
            base.OnStartup(e);

            // Создание экземпляра базовой модели представления, связанной с регистрацией
            // (В данном случае создается новый объект RegistrationWindowModel, который может управлять логикой регистрации)
            ViewModelBase viewModel = new RegistrationWindowModel();

            // Создание экземпляра окна регистрации
            RegistrationWindow registrationWindow = new RegistrationWindow();

            // Запуск окна регистрации, отображение его пользователю
            registrationWindow.Show();
        }
    }
}
