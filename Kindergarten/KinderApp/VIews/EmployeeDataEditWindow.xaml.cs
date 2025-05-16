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
    /// Логика взаимодействия для EmployeeDataEditWindow.xaml
    /// </summary>
    public partial class EmployeeDataEditWindow : Window
    {
        EmployeeDataEditWindowModel viewModel;
        public EmployeeDataEditWindow(User user, EmployeeData employeeData = null!, List<Employee> employees= null!)
        {
            InitializeComponent();
            var dbContext = DbContextSingleton.Instance.DbContext;
            viewModel = new(user, employeeData, new KinderDbContext.Services.EmployeeDataService(), employees);
            DataContext = viewModel;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
