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
    /// Логика взаимодействия для GroupEditWindow.xaml
    /// </summary>
    public partial class GroupEditWindow : Window
    {
        GroupEditWindowModel viewModel;
        public GroupEditWindow(User user, Group group = null!)
        {
            InitializeComponent();
            viewModel = new(user, group);
            DataContext = viewModel;
        }
    }
}
