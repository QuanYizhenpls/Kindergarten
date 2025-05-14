using KinderApp.Commands;
using KinderData.Entities;
using KinderData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KinderApp.ViewModels
{
    public class SalaryEditWindowModel : ViewModelBase
    {

        public SalaryEditWindowModel(User user, Salary salary, SalaryService salaryService)
        {
            Salary = salary;
            _salaryService = salaryService;
            if (salary != null)
            {
                Wage = salary.Wage;
                Bonus = salary.Bonus;
                Allowance = salary.Allowance;
                Prepayment = salary.Prepayment;
                Penalty = salary.Penalty;
                SelectedEmployee = salary.Employees;
                
            }
            
            SaveCommand = new RelayCommand(o =>
            {
                if (salary == null)
                {
                    _salaryService.Add(new Salary() {Salary_Id = Guid.NewGuid(), Wage = this.Wage, Bonus = this.Bonus, Allowance = this.Allowance, Prepayment = this.Prepayment, Penalty = this.Penalty, Employees = this.SelectedEmployee});

                }
                else
                {
                    _salaryService.Update(salary, new Salary());
                }
            });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
            _salaryService = salaryService;
        }
        private decimal wage = 0;
        private decimal bonus = 0;
        private decimal allowance = 0; 
        private decimal prepayment = 0;
        private decimal penalty = 0;
        private Employee selectedEmployee;
        private SalaryService _salaryService;
        private Salary salary;

        public Salary Salary { get => salary; set => Set(ref salary, value, nameof(salary)); }

        public decimal Wage { get => wage; set => Set(ref wage, value, nameof(wage)); }
        public decimal Bonus { get => bonus; set => Set(ref bonus, value, nameof(bonus)); }
        public decimal Allowance { get => allowance; set => Set(ref allowance, value, nameof(allowance)); }
        public decimal Prepayment { get => prepayment; set => Set(ref prepayment, value, nameof(prepayment)); }
        public decimal Penalty { get => penalty; set => Set(ref penalty, value, nameof(penalty)); }
        public Employee SelectedEmployee { get => selectedEmployee; set => Set(ref selectedEmployee, value, nameof(selectedEmployee)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
    }
}
