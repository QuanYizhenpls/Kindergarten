using KinderApp.Commands;
using KinderData.Entities;
using KinderDbContext.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderApp.ViewModels
{
    internal class EmployeeDataEditWindowModel : ViewModelBase
    {
        public EmployeeDataEditWindowModel(User user, EmployeeData employeeData, EmployeeDataService employeeDataService)
        {
            EmployeeData = employeeData;
            _employeeDataService = employeeDataService;
            if (employeeData != null)
            {
                Pasport = employeeData.Pasport;
                SNILS = employeeData.SNILS;
                INN = employeeData.INN;
                EmploymentRecord = employeeData.EmploymentRecord!;
                SelectedEmployee = EmployeeData.Employee;
                
            }
            
            SaveCommand = new RelayCommand(o =>
            {
                if (employeeData == null)
                {

                    _employeeDataService.Add(new EmployeeData() {EmployeeData_Id = Guid.NewGuid(), Pasport = this.Pasport, SNILS = this.SNILS, INN = this.INN, EmploymentRecord = this.EmploymentRecord, EmployeeId = Guid.NewGuid(), Employee = this.SelectedEmployee});

                }
                else
                {
                    _employeeDataService.Update(employeeData, new EmployeeData() { EmployeeData_Id = Guid.NewGuid(), Pasport = this.Pasport, SNILS = this.SNILS, INN = this.INN, EmploymentRecord = this.EmploymentRecord, EmployeeId = Guid.NewGuid(), Employee = this.SelectedEmployee });
                }
            });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }
        private int pasport = 0;
        private int snils = 0;
        private int inn = 0;
        private string employmentRecord = string.Empty;
        private Employee selectedEmployee;

        private EmployeeData employeeData;
        private EmployeeDataService _employeeDataService;
        public EmployeeData EmployeeData { get => employeeData; set => Set(ref employeeData, value, nameof(employeeData)); }

        public int Pasport { get => pasport; set => Set(ref pasport, value, nameof(pasport)); }
        public int SNILS { get => snils; set => Set(ref snils, value, nameof(snils)); }
        public int INN { get => inn; set => Set(ref inn, value, nameof(inn)); }
        public string EmploymentRecord { get => employmentRecord; set => Set(ref employmentRecord, value, nameof(employmentRecord)); }
        public Employee SelectedEmployee { get => selectedEmployee; set => Set(ref selectedEmployee, value, nameof(selectedEmployee)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
    }
    
}
