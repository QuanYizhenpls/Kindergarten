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
    // <summary>
    /// Окно для редактирования или создания группы (Group).
    /// </summary>
    public partial class GroupEditWindow : Window
    {
        /// <summary>
        /// Модель представления, обеспечивающая работу с группами.
        /// </summary>
        GroupEditWindowModel viewModel;

        /// <summary>
        /// Конструктор окна редактирования группы.
        /// Инициализирует UI-компоненты, модель представления и устанавливает DataContext.
        /// </summary>
        /// <param name="user">Текущий пользователь.</param>
        /// <param name="group">Группа для редактирования. Если null — создаётся новая.</param>
        public GroupEditWindow(User user, Group group = null!)
        {
            InitializeComponent();

            var dbContext = DbContextSingleton.Instance.DbContext;

            viewModel = new(user, group, new KinderDbContext.Services.GroupService());
            DataContext = viewModel;
        }

        /// <summary>
        /// Обработчик кнопки закрытия окна.
        /// Закрывает текущее окно.
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
