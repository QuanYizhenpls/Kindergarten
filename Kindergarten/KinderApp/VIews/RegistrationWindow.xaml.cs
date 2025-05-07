using KinderApp.Commands;
using KinderApp.ViewModels;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private RegistrationWindowModel _viewModel;
        public RegistrationWindow(ViewModelBase viewModel)
        {
            InitializeComponent();
            DataContext = _viewModel = new RegistrationWindowModel(viewModel);
        }
    }
}
