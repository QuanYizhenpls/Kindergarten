using KinderApp.Commands;
using KinderData.Entities;
using KinderDbContext.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinderApp.ViewModels
{
    /// <summary>
    /// Модель представления для редактирования данных сотрудника.
    /// </summary>
    public class EmployeeDataEditWindowModel : ViewModelBase
    {
        /// <summary>
        /// Инициализирует новую модель с заданными параметрами.
        /// </summary>
        /// <param name="user">Текущий пользователь (не используется в коде, возможно, для расширения).</param>
        /// <param name="employeeData">Редактируемые данные сотрудника.</param>
        /// <param name="employeeDataService">Сервис для работы с данными сотрудника.</param>
        /// <param name="employees">Список сотрудников.</param>
        public EmployeeDataEditWindowModel(User user, EmployeeData employeeData, EmployeeDataService employeeDataService, List<Employee> employees)
        {
            EmployeeData = employeeData;
            _employeeDataService = employeeDataService;
            _employees = employees;
            if (employeeData != null)
            {
                Pasport = employeeData.Pasport;
                SNILS = employeeData.SNILS;
                INN = employeeData.INN;
                EmploymentRecord = employeeData.EmploymentRecord;
                SelectedEmployee = employeeData.Employee;
            }

            SaveCommand = new RelayCommand(o =>
            {
                if (!ValidateData()) return;
                if (employeeData == null)
                {
                    _employeeDataService.Add(new EmployeeData()
                    {
                        EmployeeData_Id = Guid.NewGuid(),
                        Pasport = this.Pasport,
                        SNILS = this.SNILS,
                        INN = this.INN,
                        EmploymentRecord = this.EmploymentRecord,
                        EmployeeId = Guid.NewGuid(),
                        Employee = this.SelectedEmployee
                    });
                    MessageBox.Show($"{this.GetType().Name} - данные сотрудника добавлены!");
                }
                else
                {
                    _employeeDataService.Update(employeeData, new EmployeeData()
                    {
                        EmployeeData_Id = Guid.NewGuid(),
                        Pasport = this.Pasport,
                        SNILS = this.SNILS,
                        INN = this.INN,
                        EmploymentRecord = this.EmploymentRecord,
                        EmployeeId = Guid.NewGuid(),
                        Employee = this.SelectedEmployee
                    });
                    MessageBox.Show($"{this.GetType().Name} - данные сотрудника изменены!");
                }
            });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        private string? pasport = string.Empty;
        private string? snils = string.Empty;
        private string? inn = string.Empty;
        private string employmentRecord = string.Empty;
        private Employee selectedEmployee;

        private EmployeeData employeeData;
        private EmployeeDataService _employeeDataService;
        private List<Employee> _employees;

        /// <summary>
        /// Данные сотрудника.
        /// </summary>
        public EmployeeData EmployeeData { get => employeeData; set => Set(ref employeeData, value, nameof(employeeData)); }

        /// <summary>
        /// Паспортные данные.
        /// </summary>
        public string? Pasport
        {
            get => pasport;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Паспорт' не может быть пустым.");
                    return;
                }
                Set(ref pasport, value, nameof(pasport));
            }
        }

        /// <summary>
        /// СНИЛС.
        /// </summary>
        public string? SNILS { get => snils; set
        {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'СНИЛС' не может быть пустым.");
                    return;
                }
                Set(ref snils, value, nameof(snils)); } 
        }
        /// <summary>
        /// ИНН.
        /// </summary>
        public string? INN
        {
            get => inn;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'ИНН' не может быть пустым.");
                    return;
                }
                Set(ref inn, value, nameof(inn));
            }
        }

        /// <summary>
        /// Трудовая книжка/запись.
        /// </summary>
        public string EmploymentRecord
        {
            get => employmentRecord;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Трудовая запись' не может быть пустым.");
                    return;
                }
                Set(ref employmentRecord, value, nameof(employmentRecord));
            }
        }

        /// <summary>
        /// Выбранный сотрудник.
        /// </summary>
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                if (value == null)
                {
                    MessageBox.Show("Необходимо выбрать сотрудника.");
                    return;
                }
                Set(ref selectedEmployee, value, nameof(selectedEmployee));
            }
        }

        /// <summary>
        /// Список всех сотрудников.
        /// </summary>
        public List<Employee> Employees { get => _employees; set => Set(ref _employees, value, nameof(_employees)); }

        /// <summary>
        /// Команда сохранить изменения.
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда закрытия окна.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Валидация данных перед сохранением.
        /// </summary>
        /// <returns>Истина, если все поля корректны; иначе, ложь.</returns>
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(Pasport))
            {
                MessageBox.Show("Поле 'Паспорт' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(SNILS))
            {
                MessageBox.Show("Поле 'СНИЛС' не может быть пустым.");
                return false;

            }
            if (string.IsNullOrEmpty(INN))
            {
                MessageBox.Show("Поле 'ИНН' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(EmploymentRecord))
            {
                MessageBox.Show("Поле 'Трудовая книжка' не может быть пустым.");
                return false;
            }
            if (SelectedEmployee == null)
            {
                MessageBox.Show("Необходимо выбрать сотрудника.");
                return false;
            }
            return true;
        }
    }
}
