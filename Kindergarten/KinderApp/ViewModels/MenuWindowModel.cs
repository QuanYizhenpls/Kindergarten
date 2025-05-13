using KinderApp.Commands;
using KinderData.Entities;
using KinderApp.VIews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows;
using System.Security.AccessControl;
using KinderData.Services;
using KinderDbContext.Connections;

namespace KinderApp.ViewModels
{
    public class MenuWindowModel : ViewModelBase
    {
        public MenuWindowModel(User user, AgreementsService agreementsService, EmployeeDataService employeeDataService, EmployeeService employeeService, GroupService groupService, KindergartnerService kindergartnerService, PlanService planService, SalaryService salaryService)
        {
            CurrentUser = user;

            // Команды работы с Agreement
            AAddCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new AgreementsEditWindow(CurrentUser));
            });

            AUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddAgreement != null)
                {
                    try
                    {
                        OpenWindowDialog(new AgreementsEditWindow(CurrentUser, SelectedAddAgreement));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            ARemoveCommand = new RelayCommand(o =>
            {
                _agreementService = agreementsService;
                if (SelectedAddAgreement != null)
                {
                    try
                    {
                        _agreementService.Remove(SelectedAddAgreement);

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            // Команды работы с Employee
            EAddCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new EmployeeEditWindow(CurrentUser));
            });

            EUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddEmployee != null)
                {
                    try
                    {
                        OpenWindowDialog(new EmployeeEditWindow(CurrentUser, SelectedAddEmployee));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            ERemoveCommand = new RelayCommand(o =>
            {
                _employeeService = employeeService;
                if (SelectedAddEmployee != null)
                {
                    try
                    {
                        _employeeService.Remove(SelectedAddEmployee);

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            // Команды работы с EmployeeData
            EDAddCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new EmployeeDataEditWindow(CurrentUser));
            });

            EDUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddEmployeeData != null)
                {
                    try
                    {
                        OpenWindowDialog(new EmployeeDataEditWindow(CurrentUser, SelectedAddEmployeeData));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            EDRemoveCommand = new RelayCommand(o =>
            {
                _employeeDataService = employeeDataService;
                if (SelectedAddEmployeeData != null)
                {
                    try
                    {

                        _employeeDataService.Remove(SelectedAddEmployeeData);

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            // Команды работы с Group
            GAddCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new GroupEditWindow(CurrentUser));
            });

            GUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddGroup != null)
                {
                    try
                    {
                        OpenWindowDialog(new GroupEditWindow(CurrentUser, SelectedAddGroup));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            GRemoveCommand = new RelayCommand(o =>
            {
                _groupService = groupService;
                if (SelectedAddGroup != null)
                {
                    try
                    {
                        _groupService.Remove(SelectedAddGroup);

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            // Команды работы с Kindergartner
            KAddCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new KindergartnerEditWindow(CurrentUser));
            });

            KUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddKindergartner != null)
                {
                    try
                    {
                        OpenWindowDialog(new KindergartnerEditWindow(CurrentUser, SelectedAddKindergartner));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            KRemoveCommand = new RelayCommand(o =>
            {
                _kindergartnerService = kindergartnerService;
                if (SelectedAddKindergartner != null)
                {
                    try
                    {
                        _kindergartnerService.Remove(SelectedAddKindergartner);

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });
            // Команды работы с Plan
            PAddCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new PlanEditWindow(CurrentUser));
            });

            PUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddPlan != null)
                {
                    try
                    {
                        OpenWindowDialog(new PlanEditWindow(CurrentUser, SelectedAddPlan));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            PRemoveCommand = new RelayCommand(o =>
            {
                _planService = planService;
                if (SelectedAddPlan != null)
                {
                    try
                    {
                        _planService.Remove(SelectedAddPlan);

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            // Команды работы с Salary
            SAddCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new SalaryEditWindow(CurrentUser));
            });

            SUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddSalary != null)
                {
                    try
                    {
                        OpenWindowDialog(new SalaryEditWindow(CurrentUser, SelectedAddSalary));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            SRemoveCommand = new RelayCommand(o =>
            {
                _salaryService = salaryService;
                if (SelectedAddSalary != null)
                {
                    try
                    {
                        _salaryService.Remove(SelectedAddSalary);

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });
        }


        //поля
        private User currentUser;

        private Agreement selectedAddAgreement;
        private Employee selectedAddEmployee;
        private EmployeeData selectedAddEmployeeData;
        private Group selectedAddGroup;
        private Kindergartner selectedAddKindergartner;
        private Plan selectedAddPlan;
        private Salary selectedAddSalary;

        private AgreementsService _agreementService;
        private EmployeeService _employeeService;
        private EmployeeDataService _employeeDataService;
        private GroupService _groupService;
        private KindergartnerService _kindergartnerService;
        private PlanService _planService;
        private SalaryService _salaryService;

        public User CurrentUser
        {
            get => currentUser;
            set => Set(ref currentUser, value, nameof(User));
        }
        public Agreement SelectedAddAgreement { get => selectedAddAgreement; set => Set(ref selectedAddAgreement, value, nameof(selectedAddAgreement)); }
        public Employee SelectedAddEmployee { get => selectedAddEmployee; set => Set(ref selectedAddEmployee, value, nameof(selectedAddEmployee)); }
        public EmployeeData SelectedAddEmployeeData { get => selectedAddEmployeeData; set => Set(ref selectedAddEmployeeData, value, nameof(selectedAddEmployeeData)); }
        public Group SelectedAddGroup { get => selectedAddGroup; set => Set(ref selectedAddGroup, value, nameof(selectedAddGroup)); }
        public Kindergartner SelectedAddKindergartner { get => selectedAddKindergartner; set => Set(ref selectedAddKindergartner, value, nameof(selectedAddKindergartner)); }
        public Plan SelectedAddPlan { get => selectedAddPlan; set => Set(ref selectedAddPlan, value, nameof(selectedAddPlan)); }
        public Salary SelectedAddSalary { get => selectedAddSalary; set => Set(ref selectedAddSalary, value, nameof(selectedAddSalary)); }


        //Команды
        public RelayCommand AAddCommand { get; }
        public RelayCommand AUpdateCommand { get; }
        public RelayCommand ARemoveCommand { get; }

        public RelayCommand EAddCommand { get; }
        public RelayCommand EUpdateCommand { get; }
        public RelayCommand ERemoveCommand { get; }

        public RelayCommand EDAddCommand { get; }
        public RelayCommand EDUpdateCommand { get; }
        public RelayCommand EDRemoveCommand { get; }

        public RelayCommand GAddCommand { get; }
        public RelayCommand GUpdateCommand { get; }
        public RelayCommand GRemoveCommand { get; }

        public RelayCommand KAddCommand { get; }
        public RelayCommand KUpdateCommand { get; }
        public RelayCommand KRemoveCommand { get; }

        public RelayCommand PAddCommand { get; }
        public RelayCommand PUpdateCommand { get; }
        public RelayCommand PRemoveCommand { get; }

        public RelayCommand SAddCommand { get; }
        public RelayCommand SUpdateCommand { get; }
        public RelayCommand SRemoveCommand { get; }
    }
}
