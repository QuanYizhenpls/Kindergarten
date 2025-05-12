using KinderApp.ViewModels;
using KinderData.Entities;
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
    /// Логика взаимодействия для EmployeeEditWindow.xaml
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        EmployeeEditWindowModel viewModel;
        public EmployeeEditWindow(User user, Employee employee = null!)
        {
            InitializeComponent();
            viewModel = new(user, employee);
            DataContext = viewModel;
        }
    }
}
