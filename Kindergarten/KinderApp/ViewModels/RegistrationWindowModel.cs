using KinderApp.Commands;
using KinderData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using KinderDbContext.Services;

namespace KinderApp.ViewModels
{
    public class RegistrationWindowModel : ViewModelBase
    {
        public RegistrationWindowModel()
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
                    pervWin.Close();
                    newWin.Show();

                }
                else
                {
                    Debug.WriteLine($"[{GetType()}] - пользователь не найден!");
                    MessageBox.Show($"[{GetType()}] - пользователь не найден!");
                }
                
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

        public string Firstname
        {
            get => firstname; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Имя' не может быть пустым.");
                    return;
                }
                Set(ref firstname, value, nameof(Firstname));
            }
        }
        public string Lastname { get => lastname; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Фамилия' не может быть пустым.");
                    return;
                }
                Set(ref lastname, value, nameof(Lastname));
            }
        }
        public string Middlename { get => middlename; set => Set(ref middlename, value, nameof(Middlename)); }
        public string Login { get => login; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Логин' не может быть пустым.");
                    return;
                }
                Set(ref login, value, nameof(Login));
            }
        }
        public string Password { get => password; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Пароль' не может быть пустым.");
                    return;
                }
                Set(ref password, value, nameof(Password));
            }
        }

        public RelayCommand LoginCommand { get; }
        public RelayCommand RegisterCommand { get; }
        public RelayCommand CloseCommand { get; }

        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(Firstname))
            {
                MessageBox.Show("Поле 'Имя' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(Lastname))
            {
                MessageBox.Show("Поле 'Фамилия' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(Login))
            {
                MessageBox.Show("Поле 'Логин' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Поле 'Пароль' не может быть пустым.");
                return false;
            }
            return true;
        }
    }
}