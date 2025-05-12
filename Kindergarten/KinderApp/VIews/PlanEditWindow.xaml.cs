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
    /// Логика взаимодействия для PlanEditWindow.xaml
    /// </summary>
    public partial class PlanEditWindow : Window
    {
        PlanEditWindowModel viewModel;
        public PlanEditWindow(User user, Plan plan = null!)
        {
            InitializeComponent();
            viewModel = new(user, plan);
            DataContext = viewModel;
        }
    }
}
