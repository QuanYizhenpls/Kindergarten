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
    /// Окно для редактирования или создания записи дошкольника (Kindergartner).
    /// </summary>
    public partial class KindergartnerEditWindow : Window
    {
        /// <summary>
        /// Модель представления, управляющая логикой редактирования дошкольника.
        /// </summary>
        KindergartnerEditWindowModel viewModel;

        /// <summary>
        /// Конструктор окна редактирования дошкольника.
        /// Инициализирует компоненты интерфейса и модель представления.
        /// </summary>
        /// <param name="user">Текущий пользователь.</param>
        /// <param name="kindergartner">Объект дошкольника для редактирования, null при создании нового.</param>
        /// <param name="groups">Список групп для выбора.</param>
        public KindergartnerEditWindow(User user, Kindergartner kindergartner = null!, List<Group> groups = null!)
        {
            InitializeComponent();

            var dbContext = DbContextSingleton.Instance.DbContext;

            viewModel = new(user, kindergartner, new KinderDbContext.Services.KindergartnerService(), groups);
            DataContext = viewModel;
        }

        /// <summary>
        /// Обработчик события нажатия кнопки закрытия окна.
        /// Закрывает окно.
        /// </summary>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
