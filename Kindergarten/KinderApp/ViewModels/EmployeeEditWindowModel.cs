using KinderApp.Commands;
using KinderData.Entities;
using KinderData.Services;
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
        public EmployeeEditWindowModel(User user, Employee employee)
        {
            Employee = employee;
            if (employee != null)
            {
                FIO = employee.FIO;
                Education = employee.Education;
                Experience = employee.Experience;
                Post = employee.Post!;
                SelectedGroup = employee.Groups;
            }
            else
            {
                SelectedGroup = Groups.FirstOrDefault()!;
            }
            SaveCommand = new RelayCommand(o =>
            {
                if (employee == null)
                {
                    EmployeeService.Add(employee);

                }
                else
                {
                    EmployeeService.Update(employee, FIO, Education, Experience, Post, SelectedGroup, user);
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
        private Group selectedGroup;

        private Employee employee;

        public Employee Employee { get => employee; set => Set(ref employee, value, nameof(employee)); }

        public string FIO { get => fio; set => Set(ref fio, value, nameof(fio)); }
        public string Education { get => education; set => Set(ref education, value, nameof(education)); }
        public string Experience { get => experience; set => Set(ref experience, value, nameof(experience)); }
        public string Post { get => post; set => Set(ref post, value, nameof(post)); }
        public Group SelectedGroup { get => selectedGroup; set => Set(ref selectedGroup, value, nameof(selectedGroup)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
    }
}

