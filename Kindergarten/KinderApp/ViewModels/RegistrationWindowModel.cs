using KinderApp.Commands;
using KinderData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using KinderData.Services;

namespace KinderApp.ViewModels
{
    public class RegistrationWindowModel : ViewModelBase
    {
        public RegistrationWindowModel(ViewModelBase viewModel)
        {
            LoginCommand = new RelayCommand(o =>
            {
                var user = new UserService().GetAccount(Login, Password).Result;
                if (user != null)
                {
                    Debug.WriteLine($"[{GetType()}] - пользователь найден!");
                    MessageBox.Show($"[{GetType()}] - пользователь найден!");
                    var newWin = new MenuWindow(user);
                    var pervWin = Application.Current.MainWindow;
                    Application.Current.MainWindow = newWin;
                    newWin.Show();
                    pervWin.Close();
                }
                else Debug.WriteLine($"[{GetType()}] - пользователь не найден!");
            });



            RegisterCommand = new RelayCommand(o =>
            {
                if (new UserService().Add(new User()
                {
                    Id = Guid.NewGuid(),
                    Firstname = firstname,
                    Lastname = lastname,
                    Middlename = middlename,
                    Login = login,
                    Password = password
                }).Result)
                {
                    Debug.WriteLine($"[{GetType()}] - пользователь создан!");
                    MessageBox.Show($"[{GetType()}] - пользователь создан!");
                }
                else
                {
                    Debug.WriteLine($"[{GetType()}] - пользователь не был создан!!");
                    MessageBox.Show($"[{GetType()}] - пользователь не был создан!");
                }
            });

            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        private string firstname = string.Empty;
        private string lastname = string.Empty;
        private string middlename = string.Empty;
        private string login = string.Empty;
        private string password = string.Empty;

        public string Firstname { get => firstname; set => Set(ref firstname, value, nameof(Firstname)); }
        public string Lastname { get => lastname; set => Set(ref lastname, value, nameof(Lastname)); }
        public string Middlename { get => middlename; set => Set(ref middlename, value, nameof(Middlename)); }
        public string Login { get => login; set => Set(ref login, value, nameof(Login)); }
        public string Password { get => password; set => Set(ref password, value, nameof(Password)); }

        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }
        public RelayCommand CloseCommand { get; }
    }
}