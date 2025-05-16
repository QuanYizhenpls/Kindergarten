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
        private KinderData.Entities.Group selectedGroup;
        private Employee selectedEmployee;
        private PlanService _planService;
        private Plan plan;
        private List<Employee> _employees;
        public Plan Plan { get => plan; set => Set(ref plan, value, nameof(plan)); }

        public string DateOfTheEvent { get => dateOfTheEvent; set => Set(ref dateOfTheEvent, value, nameof(dateOfTheEvent)); }
        public string Development { get => development; set => Set(ref development, value, nameof(development)); }
        public KinderData.Entities.Group SelectedGroup { get => selectedGroup; set => Set(ref selectedGroup, value, nameof(selectedGroup)); }
        public Employee SelectedEmployee { get => selectedEmployee; set => Set(ref selectedEmployee, value, nameof(selectedEmployee)); }
        public List<Employee> Employees { get => _employees; set => Set(ref _employees, value, nameof(_employees)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
    }
}
