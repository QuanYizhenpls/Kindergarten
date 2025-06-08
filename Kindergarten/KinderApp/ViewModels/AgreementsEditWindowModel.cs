using KinderApp.Commands;
using KinderData.Entities;
using KinderDbContext.Services;
using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinderApp.ViewModels
{
    /// <summary>
    /// Модель представления для редактирования договоров.
    /// </summary>
    public class AgreementsEditWindowModel : ViewModelBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AgreementsEditWindowModel"/> с указанными параметрами.
        /// </summary>
        /// <param name="user">Текущий пользователь (не используется в коде, возможно, для дальнейшего расширения).</param>
        /// <param name="agreement">Редактируемый договор.</param>
        /// <param name="agreementsService">Сервис для работы с договорами.</param>
        /// <param name="employees">Список сотрудников.</param>
        public AgreementsEditWindowModel(User user, Agreement agreement, AgreementsService agreementsService, List<Employee> employees)
        {
            Agreement = agreement;
            _agreementService = agreementsService;
            _employees = employees;
            if (agreement != null)
            {
                Vacation = agreement.Vacation;
                SickLeave = agreement.SickLeave;
                Dismissal = agreement.Dismissal;
                EmploymentContract = agreement.EmploymentContract!;
                SelectedEmployee = agreement.Employee;
            }
            else
            {
                Vacation = "-";
                SickLeave = "-";
                Dismissal = "-";
            }

            SaveCommand = new RelayCommand(o =>
            {
                if (!ValidateData()) return;

                if (agreement == null)
                {
                    _agreementService.Add(new Agreement()
                    {
                        Agreement_Id = Guid.NewGuid(),
                        Vacation = this.Vacation,
                        SickLeave = this.SickLeave,
                        Dismissal = this.Dismissal,
                        EmploymentContract = this.EmploymentContract,
                        EmployeeId = Guid.NewGuid(),
                        Employee = this.SelectedEmployee
                    });
                    MessageBox.Show($"{this.GetType().Name} - договор добавлен!");
                }
                else
                {
                    _agreementService.Update(agreement, new Agreement()
                    {
                        Agreement_Id = Guid.NewGuid(),
                        Vacation = this.Vacation,
                        SickLeave = this.SickLeave,
                        Dismissal = this.Dismissal,
                        EmploymentContract = this.EmploymentContract,
                        EmployeeId = Guid.NewGuid(),
                        Employee = this.SelectedEmployee
                    });
                    MessageBox.Show($"{this.GetType().Name} - договор изменён!");
                }
            });

            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        private string vacation = string.Empty;
        private string sickLeave = string.Empty;
        private string dismissal = string.Empty;
        private string employmentContract = string.Empty;
        private Employee selectedEmployee;
        private List<Employee> _employees;
        private Agreement agreement;
        private AgreementsService _agreementService;

        /// <summary>
        /// Редактируемый договор.
        /// </summary>
        public Agreement Agreement { get => agreement; set => Set(ref agreement, value, nameof(agreement)); }

        /// <summary>
        /// Отпуск по договору.
        /// </summary>
        public string Vacation { get => vacation; set => Set(ref vacation, value, nameof(vacation)); }

        /// <summary>
        /// Больничный по договору.
        /// </summary>
        public string SickLeave { get => sickLeave; set => Set(ref sickLeave, value, nameof(sickLeave)); }

        /// <summary>
        /// Увольнение по договору.
        /// </summary>
        public string Dismissal { get => dismissal; set => Set(ref dismissal, value, nameof(dismissal)); }

        /// <summary>
        /// Трудовой договор.
        /// </summary>
        public string EmploymentContract
        {
            get => employmentContract; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Трудовой договор' не может быть пустым.");
                    return;
                }
                Set(ref employmentContract, value, nameof(employmentContract));
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
        /// Список сотрудников.
        /// </summary>
        public List<Employee> Employees { get => _employees; set => Set(ref _employees, value, nameof(_employees)); }

        /// <summary>
        /// Команда сохранения изменений.
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда закрытия окна.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Проверяет валидность данных перед сохранением.
        /// </summary>
        /// <returns>True, если данные валидны; иначе, False.</returns>
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(EmploymentContract))
            {
                MessageBox.Show("Поле 'Трудовой договор' не может быть пустым.");
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