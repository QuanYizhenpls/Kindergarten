using KinderApp.Commands;
using KinderData.Entities;
using KinderDbContext.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KinderApp.ViewModels
{
    /// <summary>
    /// Модель представления для окна редактирования или добавления информации о зарплате сотрудника.
    /// Обеспечивает свойства и команды для взаимодействия с пользователем и управления данными зарплаты.
    /// </summary>
    public class SalaryEditWindowModel : ViewModelBase
    {
        /// <summary>
        /// Конструктор модели представления.
        /// Инициализирует свойства модели исходя из переданных параметров.
        /// Настраивает команды сохранения и закрытия окна.
        /// </summary>
        /// <param name="user">Текущий пользователь системы (не используется явно в данном классе, но может быть расширен).</param>
        /// <param name="salary">Объект зарплаты для редактирования. Если null, создается новый объект.</param>
        /// <param name="salaryService">Сервис для работы с зарплатами, предоставляющий методы добавления и обновления.</param>
        /// <param name="employees">Список сотрудников для выбора в интерфейсе.</param>
        public SalaryEditWindowModel(User user, Salary salary, SalaryService salaryService, List<Employee> employees)
        {
            Salary = salary;
            _salaryService = salaryService;
            _employees = employees;

            // Если объект зарплаты передан (редактирование), заполняем поля текущими значениями
            if (salary != null)
            {
                Wage = salary.Wage;
                Bonus = salary.Bonus;
                Allowance = salary.Allowance;
                Prepayment = salary.Prepayment;
                Penalty = salary.Penalty;
                SelectedEmployee = salary.Employee;
            }

            // Команда сохранения данных (создание или обновление записи)
            SaveCommand = new RelayCommand(o =>
            {
                // Проверка корректности введенных данных
                if (!ValidateData()) return;

                if (salary == null)
                {
                    // Создание и добавление нового объекта зарплаты
                    _salaryService.Add(new Salary()
                    {
                        Salary_Id = Guid.NewGuid(),
                        Wage = this.Wage,
                        Bonus = this.Bonus,
                        Allowance = this.Allowance,
                        Prepayment = this.Prepayment,
                        Penalty = this.Penalty,
                        EmployeeId = Guid.NewGuid(),
                        Employee = this.SelectedEmployee
                    });
                    MessageBox.Show($"{this.GetType().Name} - зарплата добавлена!");
                }
                else
                {
                    // Обновление существующего объекта зарплаты
                    _salaryService.Update(salary, new Salary()
                    {
                        Salary_Id = Guid.NewGuid(),
                        Wage = this.Wage,
                        Bonus = this.Bonus,
                        Allowance = this.Allowance,
                        Prepayment = this.Prepayment,
                        Penalty = this.Penalty,
                        EmployeeId = Guid.NewGuid(),
                        Employee = this.SelectedEmployee
                    });
                    MessageBox.Show($"{this.GetType().Name} - зарплата изменена!");
                }
            });

            // Команда закрытия окна без сохранения
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });

            // Повторное присвоение сервиса (либо баг, либо обеспечение корректности)
            _salaryService = salaryService;
        }

        // Приватные поля для хранения значений свойств модели
        private decimal wage = 0;
        private decimal bonus = 0;
        private decimal allowance = 0;
        private decimal prepayment = 0;
        private decimal penalty = 0;

        private Employee selectedEmployee;
        private SalaryService _salaryService;
        private Salary salary;
        private List<Employee> _employees;

        /// <summary>
        /// Свойство текущей зарплаты (объект модели).
        /// Используется для двунаправленной привязки в UI.
        /// </summary>
        public Salary Salary
        {
            get => salary;
            set => Set(ref salary, value, nameof(salary));
        }

        /// <summary>
        /// Оклад сотрудника.
        /// Значение не может быть отрицательным. При попытке выставить отрицательное
        /// значение выдаётся сообщение об ошибке и значение игнорируется.
        /// </summary>
        public decimal Wage
        {
            get => wage;
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("Оклад не может быть отрицательным.");
                    return;
                }
                Set(ref wage, value, nameof(wage));
            }
        }

        /// <summary>
        /// Премия сотрудника.
        /// Не должна быть отрицательной. В случае ошибки выводится сообщение.
        /// </summary>
        public decimal Bonus
        {
            get => bonus;
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("Премия не может быть отрицательной.");
                    return;
                }
                Set(ref bonus, value, nameof(bonus));
            }
        }

        /// <summary>
        /// Надбавка сотрудника.
        /// Значение не может быть отрицательным. Контролируется с выводом предупреждения.
        /// </summary>
        public decimal Allowance
        {
            get => allowance;
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("Надбавка не может быть отрицательной.");
                    return;
                }
                Set(ref allowance, value, nameof(allowance));
            }
        }

        /// <summary>
        /// Аванс по зарплате.
        /// Не допускается отрицательное значение, при попытке - показывается предупреждение.
        /// </summary>
        public decimal Prepayment
        {
            get => prepayment;
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("Аванс не может быть отрицательным.");
                    return;
                }
                Set(ref prepayment, value, nameof(prepayment));
            }
        }

        /// <summary>
        /// Штраф, который не может быть отрицательным числом.
        /// Предполагается ввод в виде положительного значения.
        /// </summary>
        public decimal Penalty
        {
            get => penalty;
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("Запишите штраф в виде положительного числа.");
                    return;
                }
                Set(ref penalty, value, nameof(penalty));
            }
        }

        /// <summary>
        /// Выбранный сотрудник, к которому привязана зарплата.
        /// Обязательное поле, не допускает null.
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
        /// Список сотрудников для выбора в UI.
        /// Используется для заполнения элементов управления выбора сотрудника.
        /// </summary>
        public List<Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value, nameof(Employees));
        }

        /// <summary>
        /// Команда для сохранения изменений в объекте зарплаты (создание или обновление).
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда для закрытия окна редактирования без сохранения.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Валидация данных перед сохранением.
        /// Проверяет заполненность обязательных полей.
        /// </summary>
        /// <returns>Истина, если все данные корректны; иначе - ложь.</returns>
        private bool ValidateData()
        {
            if (Wage == 0)
            {
                MessageBox.Show("Поле 'Оклад' не может быть пустым.");

                return false;
            }

            if (Wage < 0)
            {
                MessageBox.Show("Оклад не может быть отрицательным.");
                return false;
            }

            if (Bonus < 0)
            {
                MessageBox.Show("Премия не может быть отрицательной.");
                return false;
            }

            if (Allowance < 0)
            {
                MessageBox.Show("Надбавка не может быть отрицательной.");
                return false;
            }
            if (Prepayment == 0)
            {
                MessageBox.Show("Поле 'Аванс' не может быть пустым.");
                return false;
            }
            if (Prepayment < 0)
            {
                MessageBox.Show("Аванс не может быть отрицательным.");
                return false;
            }
            if (Penalty < 0)
            {
                MessageBox.Show("Запишите штраф в виде положительного числа.");
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
