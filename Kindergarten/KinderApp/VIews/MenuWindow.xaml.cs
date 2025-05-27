using KinderApp.ViewModels;
using KinderData.Entities;
using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KinderApp
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml.
    /// Представляет окно меню, отображающее информацию о текущем пользователе и предоставляющее доступ к различным функциям системы.
    /// </summary>
    public partial class MenuWindow : Window
    {
        /// <summary>
        /// Модель представления, реализующая логику работы окна меню.
        /// </summary>
        MenuWindowModel viewModel;

        /// <summary>
        /// Конструктор окна меню.
        /// Инициализирует компоненты окна, создает модель представления с необходимыми сервисами и устанавливает DataContext.
        /// Устанавливает заголовок окна с именем пользователя.
        /// </summary>
        /// <param name="user">Текущий авторизованный пользователь.</param>
        public MenuWindow(User user)
        {
            InitializeComponent();

            // Инициализация модели представления с передачей сервисов для работы с данными
            viewModel = new MenuWindowModel(
                user,
                new KinderDbContext.Services.AgreementsService(),
                new KinderDbContext.Services.EmployeeDataService(),
                new KinderDbContext.Services.EmployeeService(),
                new KinderDbContext.Services.GroupService(),
                new KinderDbContext.Services.KindergartnerService(),
                new KinderDbContext.Services.PlanService(),
                new KinderDbContext.Services.SalaryService()
            );

            // Установка модели представления для привязки данных в UI
            DataContext = viewModel;

            // Установка заголовка окна с полным именем пользователя
            Title = $"Окно пользователя: {user.Fullname}";
        }
    }
}
