using KinderApp.Commands;
using KinderData.Entities;
using KinderDbContext.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace KinderApp.ViewModels
{
    /// <summary>
    /// Модель представления для редактирования данных сотрудника. 
    /// Предоставляет свойства для отображения и редактирования информации о сотруднике, а также команды для сохранения и закрытия.
    /// </summary>
    public class EmployeeEditWindowModel : ViewModelBase
    {
        /// <summary>
        /// Инициализирует новую модель с указанными параметрами, заполняя свойства текущими данными сотрудника при необходимости.
        /// </summary>
        /// <param name="user">Пользователь, вызывающий редактирование (по умолчанию не используется, предназначен для расширения).</param>
        /// <param name="employee">Данные сотрудника для редактирования; если null, создается новая запись.</param>
        /// <param name="employeeService">Сервис для работы с данными сотрудников, обеспечивает добавление и обновление.</param>
        /// <param name="groups">Список групп, в которых может находиться сотрудник.</param>
        public EmployeeEditWindowModel(User user, Employee employee, EmployeeService employeeService, List<KinderData.Entities.Group> groups)
        {
            // Инициализация полей
            Employee = employee;
            _employeeService = employeeService;
            _groups = groups;

            // Если переданы текущие данные сотрудника, заполняем свойства для отображения
            if (employee != null)
            {
                FIO = employee.FIO;
                Education = employee.Education;
                Experience = employee.Experience;
                Post = employee.Post!;
                SelectedGroup = Employee.Group;
            }
            else
            {
                Experience = "-";
            }

            // Команда для сохранения данных сотрудника
            SaveCommand = new RelayCommand(o =>
            {
                // Валидируем заполненность обязательных полей
                if (!ValidateData()) return;

                if (employee == null)
                {
                    // Добавляем нового сотрудника через сервис
                    _employeeService.Add(new Employee()
                    {
                        Employee_Id = Guid.NewGuid(),
                        FIO = this.FIO,
                        Education = this.Education,
                        Experience = this.Experience,
                        Post = this.Post,
                        GroupId = Guid.NewGuid(),
                        Group = this.SelectedGroup
                    });
                    MessageBox.Show($"{this.GetType().Name} - сотрудник добавлен!");
                }
                else
                {
                    // Обновляем существующего сотрудника
                    _employeeService.Update(employee, new Employee()
                    {
                        Employee_Id = Guid.NewGuid(),
                        FIO = this.FIO,
                        Education = this.Education,
                        Experience = this.Experience,
                        Post = this.Post,
                        GroupId = Guid.NewGuid(),
                        Group = this.SelectedGroup
                    });
                    MessageBox.Show($"{this.GetType().Name} - сотрудник изменён!");
                }
            });

            // Команда для закрытия окна
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        // Приватные поля для хранения значений свойств
        private string fio = string.Empty;
        private string education = string.Empty;
        private string experience = string.Empty;
        private string post = string.Empty;
        private KinderData.Entities.Group selectedGroup;
        private EmployeeService _employeeService;
        private Employee employee;
        private List<KinderData.Entities.Group> _groups;

        /// <summary>
        /// Данные сотрудника (модель).
        /// </summary>
        public Employee Employee { get => employee; set => Set(ref employee, value, nameof(employee)); }

        /// <summary>
        /// Поле для ввода ФИО сотрудника.
        /// Обязательное поле, отключает сохранение и показывает сообщение, если пустое.
        /// </summary>
        public string FIO { get => fio; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'ФИО' не может быть пустым.");
                    return;
                }
                Set(ref fio, value, nameof(fio)); } }

        /// <summary>
        /// Поле для ввода уровня образования сотрудника.
        /// Обязательное, не может быть пустым.
        /// </summary>
        public string Education
        {
            get => education; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Образование' не может быть пустым.");
                    return;
                }
                Set(ref education, value, nameof(Education));
            }
        }

        /// <summary>
        /// Поле для ввода опыта работы.
        /// Не обязательное, может быть пустым.
        /// </summary>
        public string Experience { get => experience; set => Set(ref experience, value, nameof(Experience)); }

        /// <summary>
        /// Поле для ввода должности сотрудника.
        /// Обязательное, сообщение о необходимости заполнения, если пустое.
        /// </summary>
        public string Post
        {
            get => post; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Должность' не может быть пустым.");
                    return;
                }
                Set(ref post, value, nameof(Post));
            }
        }

        /// <summary>
        /// Выбранная группа, к которой принадлежит сотрудник.
        /// Обязательное поле, сообщение о необходимости выбора.
        /// </summary>
        public KinderData.Entities.Group SelectedGroup
        {
            get => selectedGroup; set
            {
                if (value == null)
                {
                    MessageBox.Show("Необходимо выбрать группу.");
                    return;
                }
                Set(ref selectedGroup, value, nameof(SelectedGroup));
            }
        }

        /// <summary>
        /// Список групп, доступных для выбора.
        /// </summary>
        public List<KinderData.Entities.Group> Groups { get => _groups; set => Set(ref _groups, value, nameof(Groups)); }

        /// <summary>
        /// Команда для сохранения изменений и добавления/обновления сотрудника.
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда для закрытия окна редактирования.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Валидирует все обязательные поля, показывает сообщения, если есть ошибки.
        /// </summary>
        /// <returns><c>true</c>, если все поля заполнены корректно; иначе <c>false</c>.</returns>
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(FIO))
            {
                MessageBox.Show("Поле 'ФИО' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(Education))
            {
                MessageBox.Show("Поле 'Образование' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(Post))
            {
                MessageBox.Show("Поле 'Должность' не может быть пустым.");
                return false;
            }
            if (SelectedGroup == null)
            {
                MessageBox.Show("Необходимо выбрать группу.");
                return false;
            }
            return true;
        }
    }
}

