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
            viewModel = new MenuWindowModel(user, new KinderData.Services.AgreementsService((new SQLServerDbContext())), new KinderData.Services.EmployeeDataService(new SQLServerDbContext()), new KinderData.Services.EmployeeService(new SQLServerDbContext()), new KinderData.Services.GroupService(new SQLServerDbContext()), new KinderData.Services.KindergartnerService(new SQLServerDbContext()), new KinderData.Services.PlanService(new SQLServerDbContext()), new KinderData.Services.SalaryService(new SQLServerDbContext()));
            DataContext = viewModel;
            Title = $"Окно пользователя: {user.Fullname}";
        }
    }
}
