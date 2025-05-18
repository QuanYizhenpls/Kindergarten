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
    public class PlanEditWindowModel : ViewModelBase
    {
        public PlanEditWindowModel(User user, Plan plan, PlanService planService, List<Employee> employees)
        {
            Plan = plan;
            _planService = planService;
            _employees = employees;
            if (plan != null)
            {
                DateOfTheEvent = plan.DateOfTheEvent;
                Development = plan.Development;
                SelectedEmployee = plan.Employee;
                
            }
            
            SaveCommand = new RelayCommand(o =>
            {
                if (!ValidateData()) return;
                if (plan == null)
                {
                    _planService.Add(new Plan() {Plan_Id = Guid.NewGuid(), DateOfTheEvent = this.DateOfTheEvent, Development = this.Development, EmployeeId = Guid.NewGuid(), Employee = this.SelectedEmployee});
                    MessageBox.Show($"{this.GetType().Name} - план добавлен!");

                }
                else
                {
                    _planService.Update(plan, new Plan() { Plan_Id = Guid.NewGuid(), DateOfTheEvent = this.DateOfTheEvent, Development = this.Development, EmployeeId = Guid.NewGuid(), Employee = this.SelectedEmployee });
                    MessageBox.Show($"{this.GetType().Name} - план изменён!");
                }
            });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }
        private string dateOfTheEvent = string.Empty;
        private string development = string.Empty;
        private Employee selectedEmployee;
        private PlanService _planService;
        private Plan plan;
        private List<Employee> _employees;
        public Plan Plan { get => plan; set => Set(ref plan, value, nameof(plan)); }

        public string DateOfTheEvent { get => dateOfTheEvent; set {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Дата проведения плана' не может быть пустым.");
                    return;
                }
                Set(ref dateOfTheEvent, value, nameof(dateOfTheEvent)); } }
        public string Development { get => development; set {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Описание плана' не может быть пустым.");
                    return;
                }
                Set(ref development, value, nameof(development)); } }
        public Employee SelectedEmployee { get => selectedEmployee; set
            {
                if (value == null)
                {
                    MessageBox.Show("Необходимо выбрать сотрудника.");
                    return;
                }
                Set(ref selectedEmployee, value, nameof(selectedEmployee));
            }
        }
        public List<Employee> Employees { get => _employees; set { Set(ref _employees, value, nameof(_employees)); } }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(DateOfTheEvent))
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
