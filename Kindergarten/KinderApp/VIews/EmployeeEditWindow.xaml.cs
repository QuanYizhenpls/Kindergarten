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
    /// Окно для редактирования или создания сотрудника (Employee).
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        /// <summary>
        /// Модель представления для данной формы, отвечающая за логику редактирования сотрудника.
        /// </summary>
        EmployeeEditWindowModel viewModel;

        /// <summary>
        /// Конструктор окна редактирования сотрудника.
        /// Загружает необходимые данные и инициализирует модель представления.
        /// </summary>
        /// <param name="user">Текущий пользователь системы.</param>
        /// <param name="employee">Сотрудник для редактирования, если null — создаётся новый.</param>
        /// <param name="groups">Список групп для выбора сотрудника.</param>
       public EmployeeEditWindow(User user, Employee employee = null!, List<Group> groups = null!)
       {
            InitializeComponent();

           var dbContext = DbContextSingleton.Instance.DbContext;

           viewModel = new (user, employee, new KinderDbContext.Services.EmployeeService(), groups);
           DataContext = viewModel;
       }

          /// <summary>
          /// Обработчик кнопки закрытия окна.
         /// Закрывает окно.
          /// </summary>
          private void CloseButton_Click(object sender, RoutedEventArgs e)
          {
              Close();
          }
    }
}
