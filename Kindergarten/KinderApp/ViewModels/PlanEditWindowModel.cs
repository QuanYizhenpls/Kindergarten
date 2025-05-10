using KinderApp.Commands;
using KinderData.Entities;
using KinderData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KinderApp.ViewModels
{
    public class PlanEditWindowModel : ViewModelBase
    {
        public PlanEditWindowModel(User user, Plan plan)
        {
            Plan = plan;
            if (plan != null)
            {
                DateOfTheEvent = plan.DateOfTheEvent;
                Development = plan.Development;
                SelectedGroup = plan.Groups;
                SelectedEmployee = plan.Employees;
            }
            else
            {
                SelectedGroup = Groups.FirstOrDefault()!;
                SelectedEmployee = Employees.FirstOrDefault()!;
            }
            SaveCommand = new RelayCommand(o =>
            {
                if (plan == null)
                {
                    PlanService.Add(plan);

                }
                else
                {
                    PlanService.Update(plan, DateOfTheEvent, Development, SelectedGroup, SelectedEmployee, user);
                }
            });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }
        private string dateOfTheEvent = string.Empty;
        private string development = string.Empty;
        private Group selectedGroup;
        private Employee selectedEmployee;

        private Plan plan;

        public Plan Plan { get => plan; set => Set(ref plan, value, nameof(plan)); }

        public string DateOfTheEvent { get => dateOfTheEvent; set => Set(ref dateOfTheEvent, value, nameof(dateOfTheEvent)); }
        public string Development { get => development; set => Set(ref development, value, nameof(development)); }
        public Group SelectedGroup { get => selectedGroup; set => Set(ref selectedGroup, value, nameof(selectedGroup)); }
        public Employee SelectedEmployee { get => selectedEmployee; set => Set(ref selectedEmployee, value, nameof(selectedEmployee)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
    }
}
