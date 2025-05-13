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
    /// Логика взаимодействия для KindergartnerEditWindow.xaml
    /// </summary>
    public partial class KindergartnerEditWindow : Window
    {
        KindergartnerEditWindowModel viewModel;
        public KindergartnerEditWindow(User user, Kindergartner kindergartner = null!)
        {
            InitializeComponent();
            viewModel = new(user, kindergartner, new KinderData.Services.KindergartnerService(new SQLServerDbContext()));
            DataContext = viewModel;
        }
    }
}
