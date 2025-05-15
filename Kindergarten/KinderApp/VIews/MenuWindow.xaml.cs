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
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        MenuWindowModel viewModel;
        public MenuWindow(User user)
        {
            InitializeComponent();

            var dbContext = DbContextSingleton.Instance.DbContext;

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

            DataContext = viewModel;
            Title = $"Окно пользователя: {user.Fullname}";
        }
    }
}
