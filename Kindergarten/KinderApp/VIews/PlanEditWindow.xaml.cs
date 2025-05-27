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
    /// Логика взаимодействия для PlanEditWindow.xaml.
    /// Окно для редактирования или добавления плана учреждения.
    /// </summary>
    public partial class PlanEditWindow : Window
    {
        /// <summary>
        /// Модель представления для этого окна, реализующая логику редактирования плана.
        /// </summary>
        PlanEditWindowModel viewModel;

        /// <summary>
        /// Конструктор окна редактирования плана.
        /// Инициализирует компоненты, создает модель с нужными сервисами и устанавливает DataContext.
        /// </summary>
        /// <param name="user">Текущий пользователь системы.</param>
        /// <param name="plan">Объект плана для редактирования, null при создании нового.</param>
        /// <param name="employees">Список сотрудников, участвующих в плане.</param>
        public PlanEditWindow(User user, Plan plan = null!, List<Employee> employees = null!)
        {
            InitializeComponent();

            // Получение экземпляра DbContext
            var dbContext = DbContextSingleton.Instance.DbContext;

            // Создание модели представления с передачей сервиса для работы с планами
            viewModel = new(user, plan, new KinderDbContext.Services.PlanService(), employees);

            // Установка DataContext для привязки данных
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
