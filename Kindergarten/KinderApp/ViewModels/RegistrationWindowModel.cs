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
            
            RegisterCommand = new RelayCommand(o =>
            {
                if (new UserService().Add(new User()
                {
                    Id = Guid.NewGuid(),
                    Firstname = _firstname,
                    Lastname = _lastname,
                    Middlename = _middlename,
                    Login = _login,
                    Password = _password
                }).Result)
                {
                    Debug.WriteLine($"[{GetType()}] - user has created!");
                    MessageBox.Show($"[{GetType()}] - user has created!");
                }
                else
                {
                    Debug.WriteLine($"[{GetType()}] - user doesn't created!");
                    MessageBox.Show($"[{GetType()}] - user doesn't created!");
                }
            });
        }

        #region Variables

        #region Fields
        private string _firstname = string.Empty;
        private string _lastname = string.Empty;
        private string _middlename = string.Empty;
        private string _login = string.Empty;
        private string _password = string.Empty;
        #endregion

        #region Property
        public string Firstname
        {
            get => _firstname;
            set => Set(ref _firstname, value, nameof(Firstname));
        }
        public string Lastname
        {
            get => _lastname;
            set => Set(ref _lastname, value, nameof(Lastname));
        }
        public string Middlename
        {
            get => _middlename;
            set => Set(ref _middlename, value, nameof(Middlename));
        }
        public string Login
        {
            get => _login;
            set => Set(ref _login, value, nameof(Login));
        }
        public string Password
        {
            get => _password;
            set => Set(ref _password, value, nameof(Password));
        }

        #endregion

        #region Commands
        public RelayCommand SwitchToLoginCommand { get; }
        public RelayCommand RegisterCommand { get; }
        #endregion

        #endregion
    }
}