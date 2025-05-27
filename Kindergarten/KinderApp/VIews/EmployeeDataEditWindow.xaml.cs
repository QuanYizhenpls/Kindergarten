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
    /// Окно для редактирования или создания данных сотрудника (EmployeeData).
    /// </summary>
    public partial class EmployeeDataEditWindow : Window
    {
        /// <summary>
        /// Модель представления для данного окна, обеспечивающая логику работы с данными сотрудника.
        /// </summary>
        EmployeeDataEditWindowModel viewModel;

        /// <summary>
        /// Конструктор окна редактирования данных сотрудника.
        /// Загружает сервис, инициализирует модель представления и устанавливает DataContext.
        /// </summary>
        /// <param name="user">Текущий пользователь системы.</param>
        /// <param name="employeeData">Данные сотрудника для редактирования, может быть null для создания новых данных.</param>
        /// <param name="employees">Список сотрудников для выбора.</param>
        public EmployeeDataEditWindow(User user, EmployeeData employeeData = null!, List<Employee> employees = null!)
        {
            InitializeComponent();

            // Получение экземпляра DbContext (если требуется для инициализации модели)
            var dbContext = DbContextSingleton.Instance.DbContext;

            // Инициализация модели представления с передачей сервиса и параметров
            viewModel = new(user, employeeData, new KinderDbContext.Services.EmployeeDataService(), employees);
            DataContext = viewModel;
        }

        /// <summary>
        /// Обработчик нажатия кнопки Закрыть.
        /// Закрывает текущее окно.
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
