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
    /// Модель представления для окна редактирования/добавления плана.
    /// Предоставляет свойства и команды для взаимодействия с интерфейсом.
    /// </summary>
    public class PlanEditWindowModel : ViewModelBase
    {
        /// <summary>
        /// Конструктор модели с параметрами пользователя, плана, сервиса планов и списка сотрудников.
        /// Инициализирует свойства и команды для работы в интерфейсе.
        /// </summary>
        /// <param name="user">Текущий пользователь системы.</param>
        /// <param name="plan">Объект плана, если редактирование существующего. При добавлении может быть null.</param>
        /// <param name="planService">Сервис для операций с планами (добавление, обновление).</param>
        /// <param name="employees">Список сотрудников, доступных для выбора при редактировании плана.</param>
        public PlanEditWindowModel(User user, Plan plan, PlanService planService, List<Employee> employees)
        {
            // Инициализация свойства плана текущим переданным объектом
            Plan = plan;
            // Сохранение ссылки на сервис планов
            _planService = planService;
            // Загрузка списка сотрудников
            _employees = employees;

            // Если передан существующий план, заполняем свойства для отображения
            if (plan != null)
            {
                DateOfTheEvent = plan.DateOfTheEvent;
                Development = plan.Development;
                SelectedEmployee = plan.Employee;
            }

            // Инициализация команды для сохранения данных
            SaveCommand = new RelayCommand(o =>
            {
                // Проверка корректности данных перед сохранением
                if (!ValidateData()) return;

                if (plan == null)
                {
                    // Создание нового плана и добавление его через сервис
                    _planService.Add(new Plan()
                    {
                        Plan_Id = Guid.NewGuid(),
                        DateOfTheEvent = this.DateOfTheEvent,
                        Development = this.Development,
                        EmployeeId = Guid.NewGuid(),
                        Employee = this.SelectedEmployee
                    });
                    MessageBox.Show($"{this.GetType().Name} - план добавлен!");
                }
                else
                {
                    // Обновление существующего плана
                    _planService.Update(plan, new Plan()
                    {
                        Plan_Id = Guid.NewGuid(),
                        DateOfTheEvent = this.DateOfTheEvent,
                        Development = this.Development,
                        EmployeeId = Guid.NewGuid(),
                        Employee = this.SelectedEmployee
                    });
                    MessageBox.Show($"{this.GetType().Name} - план изменён!");
                }
            });

            // Инициализация команды для закрытия окна
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        // Частные поля для хранения данных
        private DateTime dateOfTheEvent; // Дата проведения плана
        private string development = string.Empty; // Описание плана
        private Employee selectedEmployee; // выбранный сотрудник
        private PlanService _planService; // сервис для работы с планами
        private Plan plan; // текущий редактируемый или создаваемый план
        private List<Employee> _employees; // список сотрудников

        /// <summary>
        /// Свойство для хранения объекта плана. Используется для привязки и отображения.
        /// </summary>
        public Plan Plan
        {
            get => plan;
            set => Set(ref plan, value, nameof(plan));
        }

        /// <summary>
        /// Дата проведения плана. Свойство с проверкой входных данных.
        /// </summary>
        public DateTime DateOfTheEvent { get => dateOfTheEvent; 
                set { if (value == null)
                {
                    MessageBox.Show("Поле 'Дата проведения' не может быть пустой.");
                    return;
                } Set(ref dateOfTheEvent, value, nameof(dateOfTheEvent)); } }
        /// <summary>
        /// Описание плана. Свойство с проверкой на пустое значение.
        /// </summary>
        public string Development
        {
            get => development;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Описание плана' не может быть пустым.");
                    return;
                }
                Set(ref development, value, nameof(development));
            }
        }

        /// <summary>
        /// Выбранный сотрудник, связанный с планом. Проверка на null.
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
        /// Список сотрудников для выбора. Связь с UI для отображения и выбора.
        /// </summary>
        public List<Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value, nameof(Employees));
        }

        /// <summary>
        /// Команда для сохранения данных (добавление или обновление).
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда для закрытия окна.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Валидация данных перед сохранением.
        /// Проверяет заполненность обязательных полей.
        /// </summary>
        /// <returns>Истина, если все данные корректны; иначе - ложь.</returns>
        private bool ValidateData()
        {
            if (DateOfTheEvent == null)
            {
                MessageBox.Show("Поле 'Дата проведения плана' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(Development))
            {
                MessageBox.Show("Поле 'Описание плана' не может быть пустым.");
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
