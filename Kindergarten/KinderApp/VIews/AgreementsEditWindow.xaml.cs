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
    /// Окно для редактирования или создания соглашений (Agreements).
    /// </summary>
    public partial class AgreementsEditWindow : Window
    {
        /// <summary>
        /// Модель представления для данного окна, реализующая логику взаимодействия с данными соглашения.
        /// </summary>
        AgreementsEditWindowModel viewModel;

        /// <summary>
        /// Конструктор окна соглашений.
        /// Инициализирует компоненты интерфейса, модель представления с необходимыми параметрами и устанавливает DataContext.
        /// </summary>
        /// <param name="user">Текущий пользователь системы.</param>
        /// <param name="agreement">Соглашение для редактирования, если null — создаётся новое.</param>
        /// <param name="employees">Список сотрудников для выбора.</param>
        public AgreementsEditWindow(User user, Agreement agreement = null!, List<Employee> employees = null!)
        {
            InitializeComponent();
            // Инициализация модели представления с передачей сервиса и параметров
            viewModel = new(user, agreement, new KinderDbContext.Services.AgreementsService(), employees);
            // Установка DataContext для привязки данных в интерфейсе
            DataContext = viewModel;
        }

        /// <summary>
        /// Обработчик нажатия кнопки закрытия окна.
        /// Закрывает текущее окно.
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
