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
    public class SalaryEditWindowModel : ViewModelBase
    {
        public SalaryEditWindowModel(User user, Salary salary, SalaryService salaryService, List<Employee> employees)
        {
            Salary = salary;
            _salaryService = salaryService;
            _employees = employees;
            if (salary != null)
            {
                Wage = salary.Wage;
                Bonus = salary.Bonus;
                Allowance = salary.Allowance;
                Prepayment = salary.Prepayment;
                Penalty = salary.Penalty;
                SelectedEmployee = salary.Employee;
                
            }
            
            SaveCommand = new RelayCommand(o =>
            {
                if (!ValidateData()) return;
                if (salary == null)
                {
                    _salaryService.Add(new Salary() {Salary_Id = Guid.NewGuid(), Wage = this.Wage, Bonus = this.Bonus, Allowance = this.Allowance, Prepayment = this.Prepayment, Penalty = this.Penalty, EmployeeId = Guid.NewGuid(), Employee = this.SelectedEmployee});
                    MessageBox.Show($"{this.GetType().Name} - зарплата добавлена!");
                }
                else
                {
                    _salaryService.Update(salary, new Salary() { Salary_Id = Guid.NewGuid(), Wage = this.Wage, Bonus = this.Bonus, Allowance = this.Allowance, Prepayment = this.Prepayment, Penalty = this.Penalty, EmployeeId = Guid.NewGuid(), Employee = this.SelectedEmployee });
                    MessageBox.Show($"{this.GetType().Name} - зарплата изменена!");
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
        private List<Employee> _employees;

        public Salary Salary { get => salary; set => Set(ref salary, value, nameof(salary)); }

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
        public decimal Prepayment { get => prepayment; set
            {
                if (value < 0)
                {
                    MessageBox.Show("Аванс не может быть отрицательным.");
                    return;
                }
                Set(ref prepayment, value, nameof(prepayment));
            }
        }
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
        public List<Employee> Employees { get => _employees; set => Set(ref _employees, value, nameof(_employees)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
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
