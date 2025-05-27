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
    /// <summary>
    /// Модель представления для окна регистрации.
    /// Обеспечивает свойства и команды для авторизации, регистрации и закрытия окна.
    /// </summary>
    public class RegistrationWindowModel : ViewModelBase
    {
        /// <summary>
        /// Конструктор модели, инициализирует команды для входа, регистрации и закрытия окна.
        /// </summary>
        public RegistrationWindowModel()
        {
            // Команда для входа пользователя в систему по логину и паролю
            LoginCommand = new RelayCommand(o =>
            {
                // Получение аккаунта пользователя по логину и паролю
                var user = new UserService().GetAccount(Login, Password).Result;
                if (user != null)
                {
                    // Пользователь найден. выводим сообщение и переключаем окно
                    Debug.WriteLine($"[{GetType()}] - пользователь найден!");
                    MessageBox.Show($"[{GetType()}] - пользователь найден!");

                    // Создаем новое окно меню для пользователя
                    var newWin = new MenuWindow(user);
                    // Запоминаем текущее главное окно
                    var pervWin = Application.Current.MainWindow;
                    // Устанавливаем новое окно как главное
                    Application.Current.MainWindow = newWin;
                    // Закрываем предыдущее окно
                    pervWin.Close();
                    // Открываем новое окно
                    newWin.Show();
                }
                else
                {
                    // Пользователь не найден. выводим сообщение
                    Debug.WriteLine($"[{GetType()}] - пользователь не найден!");
                    MessageBox.Show($"[{GetType()}] - пользователь не найден!");
                }
            });

            // Команда для регистрации нового пользователя
            RegisterCommand = new RelayCommand(o =>
            {
                // Проверка валидности введенных данных
                if (!ValidateData()) return;
                // Создаём нового пользователя и вызываем метод добавления через сервис
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
                    // Регистрация прошла успешно
                    Debug.WriteLine($"[{GetType()}] - пользователь создан!");
                    MessageBox.Show($"[{GetType()}] - пользователь создан!");
                }
                else
                {
                    // Не удалось создать пользователя
                    Debug.WriteLine($"[{GetType()}] - пользователь не был создан!!");
                    MessageBox.Show($"[{GetType()}] - пользователь не был создан!");
                }
            });

            // Команда для закрытия окна
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        // Приватные поля для хранения данных пользователя
        private string firstname = string.Empty;    // Имя
        private string lastname = string.Empty;     // Фамилия
        private string middlename = string.Empty;   // Отчество
        private string login = string.Empty;        // Логин
        private string password = string.Empty;     // Пароль

        /// <summary>
        /// Имя пользователя.
        /// При установке проверяет, чтобы оно не было пустым.
        /// </summary>
        public string Firstname
        {
            get => firstname;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Имя' не может быть пустым.");
                    return;
                }
                Set(ref firstname, value, nameof(Firstname));
            }
        }
        /// <summary>
        /// Фамилия пользователя.
        /// Проверяет, чтобы поле не было пустым.
        /// </summary>
        public string Lastname
        {
            get => lastname;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Фамилия' не может быть пустым.");
                    return;
                }
                Set(ref lastname, value, nameof(Lastname));
            }
        }

        /// <summary>
        /// Отчество пользователя.
        /// Можно установить без дополнительных ограничений.
        /// </summary>
        public string Middlename
        {
            get => middlename;
            set => Set(ref middlename, value, nameof(Middlename));
        }

        /// <summary>
        /// Логин пользователя.
        /// Проверка на пустое значение.
        /// </summary>
        public string Login
        {
            get => login;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Логин' не может быть пустым.");
                    return;
                }
                Set(ref login, value, nameof(Login));
            }
        }

        /// <summary>
        /// Пароль пользователя.
        /// Проверка на пустое значение.
        /// </summary>
        public string Password
        {
            get => password;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Пароль' не может быть пустым.");
                    return;
                }
                Set(ref password, value, nameof(Password));
            }
        }

        /// <summary>
        /// Команда для выполнения входа пользователя.
        /// </summary>
        public RelayCommand LoginCommand { get; }

        /// <summary>
        /// Команда для выполнения регистрации нового пользователя.
        /// </summary>
        public RelayCommand RegisterCommand { get; }

        /// <summary>
        /// Команда для закрытия окна регистрации.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Валидация данных перед сохранением.
        /// Проверяет заполненность обязательных полей.
        /// </summary>
        /// <returns>Истина, если все данные корректны; иначе - ложь.</returns>
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