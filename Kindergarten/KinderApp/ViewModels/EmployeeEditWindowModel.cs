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
    public class EmployeeEditWindowModel : ViewModelBase
    {
        public EmployeeEditWindowModel(User user, Employee employee, EmployeeService employeeService, List<KinderData.Entities.Group> groups)
        {
            Employee = employee;
            _employeeService = employeeService;
            _groups = groups;
            if (employee != null)
            {
                FIO = employee.FIO;
                Education = employee.Education;
                Experience = employee.Experience;
                Post = employee.Post!;
                SelectedGroup = Employee.Group;
                
            }
            
            SaveCommand = new RelayCommand(o =>
            {
                if (!ValidateData()) return;
                if (employee == null)
                {
                    _employeeService.Add(new Employee() {Employee_Id = Guid.NewGuid(), FIO = this.FIO, Education = this.Education, Experience = this.Experience, Post = this.Post, GroupId = Guid.NewGuid(), Group = this.SelectedGroup});
                    MessageBox.Show($"{this.GetType().Name} - сотрудник добавлен!");

                }
                else
                {
                    _employeeService.Update(employee, new Employee() { Employee_Id = Guid.NewGuid(), FIO = this.FIO, Education = this.Education, Experience = this.Experience, Post = this.Post, GroupId = Guid.NewGuid(), Group = this.SelectedGroup });
                    MessageBox.Show($"{this.GetType().Name} - сотрудник изменён!");
                }
            });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }
        private string fio = string.Empty;
        private string education = string.Empty;
        private string experience = string.Empty;
        private string post = string.Empty;
        private KinderData.Entities.Group selectedGroup;
        private EmployeeService _employeeService;
        private Employee employee;
        private List<KinderData.Entities.Group> _groups;

        public Employee Employee { get => employee; set => Set(ref employee, value, nameof(employee)); }

        public string FIO { get => fio; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'ФИО' не может быть пустым.");
                    return;
                }
                Set(ref fio, value, nameof(fio)); } }
        public string Education { get => education; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Образование' не может быть пустым.");
                    return;
                }
                Set(ref education, value, nameof(education)); } }
        public string Experience { get => experience; set => Set(ref experience, value, nameof(experience)); }
        public string Post { get => post; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Должность' не может быть пустым.");
                    return;
                }
                Set(ref post, value, nameof(post)); } }
        public KinderData.Entities.Group SelectedGroup { get => selectedGroup; set {
                if (value == null)
                {
                    MessageBox.Show("Необходимо выбрать группу.");
                    return;
                }
                Set(ref selectedGroup, value, nameof(selectedGroup)); } }
        public List<KinderData.Entities.Group> Groups { get => _groups; set => Set(ref _groups, value, nameof(_groups)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
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

