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

namespace KinderApp.VIews
{
    /// <summary>
    /// Логика взаимодействия для SalaryEditWindow.xaml.
    /// Окно для редактирования или создания данных о зарплате сотрудника.
    /// </summary>
    public partial class SalaryEditWindow : Window
    {
        /// <summary>
        /// Модель представления для этого окна, занимающаяся логикой изменения зарплаты.
        /// </summary>
        SalaryEditWindowModel viewModel;

        /// <summary>
        /// Конструктор окна редактирования зарплаты.
        /// Инициализирует компоненты, создает модель с сервисом зарплат и устанавливает DataContext.
        /// </summary>
        /// <param name="user">Текущий пользователь системы.</param>
        /// <param name="salary">Объект зарплаты для редактирования, null при создании новой записи.</param>
        /// <param name="employees">Список сотрудников для выбора.</param>
        public SalaryEditWindow(User user, Salary salary = null!, List<Employee> employees = null!)
        {
            InitializeComponent();

            // Получение экземпляра DbContext
            var dbContext = DbContextSingleton.Instance.DbContext;

            // Создание модели представления с передачей сервиса по работе с зарплатой
            viewModel = new(user, salary, new KinderDbContext.Services.SalaryService(), employees);

            // Установка DataContext
            DataContext = viewModel;
        }

        /// <summary>
        /// Обработчик события нажатия кнопки закрытия окна.
        /// Закрывает текущее окно.
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
