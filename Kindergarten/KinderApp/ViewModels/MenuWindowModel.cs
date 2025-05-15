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
using KinderDbContext.Services;
using KinderDbContext.Connections;

namespace KinderApp.ViewModels
{
    public class MenuWindowModel : ViewModelBase
    {
        public MenuWindowModel(User user, AgreementsService agreementsService, EmployeeDataService employeeDataService, EmployeeService employeeService, GroupService groupService, KindergartnerService kindergartnerService, PlanService planService, SalaryService salaryService)
        {
            CurrentUser = user;
            _agreementService = agreementsService;
            _employeeDataService = employeeDataService;
            _employeeService = employeeService;
            _kindergartnerService = kindergartnerService;
            _groupService = groupService;
            _planService = planService;
            _salaryService = salaryService;
            UpdateLists();

            // Команды работы с Agreement
            AAddCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new AgreementsEditWindow(CurrentUser));
                UpdateLists();
            });

            AUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddAgreement != null)
                {
                    try
                    {
                        OpenWindowDialog(new AgreementsEditWindow(CurrentUser, SelectedAddAgreement));
                        UpdateLists();
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
                if (SelectedAddAgreement != null)
                {
                    try
                    {
                        _agreementService.Remove(SelectedAddAgreement);
                        UpdateLists();

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
                UpdateLists();
            });

            EUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddEmployee != null)
                {
                    try
                    {
                        OpenWindowDialog(new EmployeeEditWindow(CurrentUser, SelectedAddEmployee));
                        UpdateLists();
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
                if (SelectedAddEmployee != null)
                {
                    try
                    {
                        _employeeService.Remove(SelectedAddEmployee);
                        UpdateLists();

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
                UpdateLists();
            });

            EDUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddEmployeeData != null)
                {
                    try
                    {
                        OpenWindowDialog(new EmployeeDataEditWindow(CurrentUser, SelectedAddEmployeeData));
                        UpdateLists();
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
                if (SelectedAddEmployeeData != null)
                {
                    try
                    {

                        _employeeDataService.Remove(SelectedAddEmployeeData);
                        UpdateLists();

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
                UpdateLists();
            });

            GUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddGroup != null)
                {
                    try
                    {
                        OpenWindowDialog(new GroupEditWindow(CurrentUser, SelectedAddGroup));
                        UpdateLists();
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
                if (SelectedAddGroup != null)
                {
                    try
                    {
                        _groupService.Remove(SelectedAddGroup);
                        UpdateLists();

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
                UpdateLists();
            });

            KUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddKindergartner != null)
                {
                    try
                    {
                        OpenWindowDialog(new KindergartnerEditWindow(CurrentUser, SelectedAddKindergartner));
                        UpdateLists();
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
                if (SelectedAddKindergartner != null)
                {
                    try
                    {
                        _kindergartnerService.Remove(SelectedAddKindergartner).GetAwaiter();
                        UpdateLists();

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
                UpdateLists();
            });

            PUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddPlan != null)
                {
                    try
                    {
                        OpenWindowDialog(new PlanEditWindow(CurrentUser, SelectedAddPlan));
                        UpdateLists();
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
                if (SelectedAddPlan != null)
                {
                    try
                    {
                        _planService.Remove(SelectedAddPlan);
                        UpdateLists();

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
                UpdateLists();
            });

            SUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddSalary != null)
                {
                    try
                    {
                        OpenWindowDialog(new SalaryEditWindow(CurrentUser, SelectedAddSalary));
                        UpdateLists();
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
                if (SelectedAddSalary != null)
                {
                    try
                    {
                        _salaryService.Remove(SelectedAddSalary);
                        UpdateLists();

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

        void UpdateLists()
        {
            Agreements = _agreementService.GetEntities().Result.ToList();
            Employees = _employeeService.GetEntities().Result.ToList();
            EmployeeDatas = _employeeDataService.GetEntities().Result.ToList();
            Groups = _groupService.GetEntities().Result.ToList();
            Kindergartners = _kindergartnerService.GetEntities().Result.ToList();
            Plans = _planService.GetEntities().Result.ToList();
            Salaries = _salaryService.GetEntities().Result.ToList();
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

        private List<Agreement> _agreements;
        private List<Employee> _employees;
        private List<EmployeeData> _employeeDatas;
        private List<Group> _groups;
        private List<Kindergartner> _kindergartners;
        private List<Plan> _plans;
        private List<Salary> _salaries;

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

        public List<Agreement> Agreements { get => _agreements; set => Set(ref _agreements, value, nameof(_agreements)); }
        public List<Employee> Employees { get => _employees; set => Set(ref _employees, value, nameof(_employees)); }
        public List<EmployeeData> EmployeeDatas { get => _employeeDatas; set => Set(ref _employeeDatas, value, nameof(_employeeDatas)); }
        public List<Group> Groups { get => _groups; set => Set(ref _groups, value, nameof(_groups)); }
        public List<Plan> Plans { get => _plans; set => Set(ref _plans, value, nameof(_plans)); }
        public List<Salary> Salaries { get => _salaries; set => Set(ref _salaries, value, nameof(_salaries)); }
        public List<Kindergartner> Kindergartners { get => _kindergartners; set => Set(ref _kindergartners, value, nameof(Kindergartners)); }

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
