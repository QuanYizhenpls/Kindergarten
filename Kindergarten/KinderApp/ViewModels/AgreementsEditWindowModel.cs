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
    public class AgreementsEditWindowModel : ViewModelBase
    {
        public AgreementsEditWindowModel(User user, Agreement agreement, AgreementsService agreementsService)
        {
            Agreement = agreement;
            _agreementService = agreementsService;
            if (agreement != null)
            {
                Vacation = agreement.Vacation;
                SickLeave = agreement.SickLeave;
                Dismissal = agreement.Dismissal;
                EmploymentContract = agreement.EmploymentContract!;
                SelectedEmployee = agreement.Employee;
                
            }
            
            SaveCommand = new RelayCommand(o =>
            {
                if (agreement == null)
                {
                    _agreementService.Add(new Agreement() {Agreement_Id = Guid.NewGuid(), Vacation = this.Vacation, SickLeave = this.SickLeave, Dismissal = this.Dismissal, EmploymentContract = this.EmploymentContract, EmployeeId = Guid.NewGuid(), Employee = this.SelectedEmployee});
                    MessageBox.Show($"{this.GetType().Name} - договор добавлен!");

                }
                else
                {

                    _agreementService.Update(agreement, new Agreement() { Agreement_Id = Guid.NewGuid(), Vacation = this.Vacation, SickLeave = this.SickLeave, Dismissal = this.Dismissal, EmploymentContract = this.EmploymentContract, EmployeeId = Guid.NewGuid(), Employee = this.SelectedEmployee });
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

        private Agreement agreement;
        private AgreementsService _agreementService;

        public Agreement Agreement { get => agreement; set => Set(ref agreement, value, nameof(agreement)); }
        
        public string Vacation { get => vacation; set => Set(ref vacation, value, nameof(vacation)); }
        public string SickLeave { get => sickLeave; set => Set(ref sickLeave, value, nameof(sickLeave)); }
        public string Dismissal { get => dismissal; set => Set(ref dismissal, value, nameof(dismissal)); }
        public string EmploymentContract { get => employmentContract; set => Set(ref employmentContract, value, nameof(employmentContract)); }
        public Employee SelectedEmployee { get => selectedEmployee; set => Set(ref selectedEmployee, value, nameof(selectedEmployee)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
    }
}
